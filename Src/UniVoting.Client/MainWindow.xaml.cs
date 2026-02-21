using System;
using System.Collections.Concurrent;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
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

        public MainWindow()
        {
            InitializeComponent();
            _positionsStack = new Stack<Position>();
            _voter = new Voter();
            _votes = new ConcurrentBag<Vote>();
            _skippedVotes = new ConcurrentBag<SkippedVotes>();
        }

        public MainWindow(Stack<Position> positionsStack, Voter voter)
        {
            InitializeComponent();
            _positionsStack = positionsStack;
            _voter = voter;
            _skippedVotes = new ConcurrentBag<SkippedVotes>();
            _votes = new ConcurrentBag<Vote>();
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

        private void YesOrNoCandidateControl_VoteNo(object? source, EventArgs args) => ProcessVote();
        private void YesOrNoCandidateControl_VoteCast(object? source, EventArgs args) => ProcessVote();
        private void CandidateControl_VoteCast(object? source, EventArgs args) => ProcessVote();

        private async void MainWindow_Loaded1(object? sender, RoutedEventArgs e)
        {
            try
            {
                var election = ElectionConfigurationService.ConfigureElection();
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
                new ClientVoteCompletedPage(_votes, _voter, _skippedVotes).Show();
                Hide();
            }
        }

        private ClientVotingPage VotingPageMaker(Stack<Position> positions)
        {
            _votingPage = new ClientVotingPage(_voter, positions.Pop(), _votes, _skippedVotes);
            _votingPage.VoteCompleted += VotingPage_VoteCompleted;
            return _votingPage;
        }

        private async void MainWindow_Loaded(object? sender, RoutedEventArgs e)
        {
            PageHolder.Content = VotingPageMaker(_positionsStack);
            _voter.VoteInProgress = true;
            await VotingService.UpdateVoter(_voter);
        }

        private void VotingPage_VoteCompleted(object? source, EventArgs args) => ProcessVote();
    }
}
