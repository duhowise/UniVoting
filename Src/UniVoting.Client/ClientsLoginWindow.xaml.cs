using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client
{
    public partial class ClientsLoginWindow : Window
    {
        private IEnumerable<Model.Position>? _positions;
        private Voter _voter;
        private readonly IElectionConfigurationService _electionService;
        private readonly IVotingService _votingService;
        private readonly ILogger _logger;
        private readonly IServiceProvider _sp;
        private readonly IClientSessionService _session;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public ClientsLoginWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public ClientsLoginWindow(IElectionConfigurationService electionService, IVotingService votingService, ILogger logger, IServiceProvider sp, IClientSessionService session)
        {
            _electionService = electionService;
            _votingService = votingService;
            _logger = logger;
            _sp = sp;
            _session = session;
            InitializeComponent();
            Loaded += ClientsLoginWindow_Loaded;
            _voter = new Voter();
            BtnGo.Click += BtnGo_Click;
        }

        protected override void OnClosing(WindowClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void ClientsLoginWindow_Loaded(object? sender, RoutedEventArgs e)
        {
            try
            {
                var election = _electionService.ConfigureElection();
                if (election?.Logo != null)
                    MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) { Opacity = 0.2 };
                if (election != null)
                {
                    VotingName.Text = election.ElectionName?.ToUpper();
                    VotingSubtitle.Content = election.EletionSubTitle?.ToUpper();
                }
                _positions = _electionService.GetAllPositions();
                _session.Positions = new Stack<Model.Position>();
                foreach (var position in _positions)
                    _session.Positions.Push(position);
            }
            catch (Exception exception)
            {
                _ = MessageBoxManager.GetMessageBoxStandard("Election Positions Error", exception.Message).ShowAsync();
            }
        }

        private async void BtnGo_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Pin.Text))
            {
                try
                {
                    _voter = await _electionService.LoginVoter(new Voter { VoterCode = Pin.Text });
                    ConfirmVoterAsync();
                }
                catch (Exception exception)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Election Login Error", exception.Message).ShowAsync();
                }
            }
        }

        public async void ConfirmVoterAsync()
        {
            if (_voter != null)
            {
                if (!_voter.VoteInProgress && !_voter.Voted)
                {
                    _session.CurrentVoter = _voter;
                    _session.Votes = new ConcurrentBag<Vote>();
                    _session.SkippedVotes = new ConcurrentBag<SkippedVotes>();
                    _sp.GetRequiredService<MainWindow>().Show();
                    Hide();
                }
                else
                {
                    await MessageBoxManager.GetMessageBoxStandard("Error Confirming Voter",
                        "Please Contact Admin Your Details May Not Be Available\n Possible Cause: You May Have Already Voted").ShowAsync();
                    Pin.Text = string.Empty;
                }
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Error Confirming Voter", "Wrong Code!").ShowAsync();
                Pin.Text = string.Empty;
            }
        }
    }
}
