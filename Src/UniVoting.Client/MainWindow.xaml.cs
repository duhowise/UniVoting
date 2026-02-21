using System;
using System.Collections.Concurrent;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    public partial class MainWindow : Window
    {
        private readonly Stack<Position> _positionsStack;
        private ClientVotingPage? _votingPage;
        private Voter _voter;
        private ConcurrentBag<Vote> _votes;
        private ConcurrentBag<SkippedVotes> _skippedVotes;
        private readonly IElectionConfigurationService _electionService;
        private readonly IVotingService _votingService;
        private readonly IServiceProvider _sp;
        private readonly IClientSessionService _session;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public MainWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public MainWindow(IClientSessionService session, IElectionConfigurationService electionService, IVotingService votingService, IServiceProvider sp)
        {
            _session = session;
            _electionService = electionService;
            _votingService = votingService;
            _sp = sp;
            _positionsStack = session.Positions;
            _voter = session.CurrentVoter!;
            _votes = session.Votes;
            _skippedVotes = session.SkippedVotes;
            InitializeComponent();
            Loaded += MainWindow_Loaded1;
            Loaded += MainWindow_Loaded;
            CandidateControl.VoteCast += CandidateControl_VoteCast;
            YesOrNoCandidateControl.VoteCast += YesOrNoCandidateControl_VoteCast;
            YesOrNoCandidateControl.VoteNo += YesOrNoCandidateControl_VoteNo;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private async void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exp = e.ExceptionObject as Exception;
            if (exp != null)
                await MessageBoxManager.GetMessageBoxStandard("Error", exp.Message).ShowAsync();
        }

        private void YesOrNoCandidateControl_VoteNo() => ProcessVote();
        private void YesOrNoCandidateControl_VoteCast() => ProcessVote();
        private void CandidateControl_VoteCast() => ProcessVote();

        private async void MainWindow_Loaded1(object? sender, RoutedEventArgs e)
        {
            try
            {
                var election = _electionService.ConfigureElection();
                if (election?.Logo != null)
                    MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) { Opacity = 0.2 };
            }
            catch { }
        }

        private void ProcessVote()
        {
            if (_positionsStack.Count != 0)
            {
                PageHolder.Content = VotingPageMaker(_positionsStack);
            }
            else
            {
                _voter.Voted = true;
                _sp.GetRequiredService<ClientVoteCompletedPage>().Show();
                Hide();
            }
        }

        private ClientVotingPage VotingPageMaker(Stack<Position> positions)
        {
            _session.CurrentPosition = positions.Pop();
            _votingPage = _sp.GetRequiredService<ClientVotingPage>();
            _votingPage.VoteCompleted += VotingPage_VoteCompleted;
            return _votingPage;
        }

        private async void MainWindow_Loaded(object? sender, RoutedEventArgs e)
        {
            PageHolder.Content = VotingPageMaker(_positionsStack);
            _voter.VoteInProgress = true;
            await _votingService.UpdateVoter(_voter);
        }

        private void VotingPage_VoteCompleted() => ProcessVote();
    }
}
