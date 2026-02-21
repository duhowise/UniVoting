using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client
{
    public partial class ClientsLoginWindow : Window
    {
        private IEnumerable<Model.Position>? _positions;
        private Stack<Model.Position> _positionsStack;
        private Voter _voter;
        private readonly IElectionConfigurationService _electionService;
        private readonly IVotingService _votingService;
        private readonly ILogger _logger;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public ClientsLoginWindow()
        {
            InitializeComponent();
            _electionService = null!;
            _votingService = null!;
            _logger = null!;
            _positionsStack = new Stack<Model.Position>();
            _voter = new Voter();
        }

        public ClientsLoginWindow(IElectionConfigurationService electionService, IVotingService votingService, ILogger logger)
        {
            _electionService = electionService;
            _votingService = votingService;
            _logger = logger;
            InitializeComponent();
            _positionsStack = new Stack<Model.Position>();
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
                foreach (var position in _positions)
                    _positionsStack.Push(position);
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
                    new MainWindow(_positionsStack, _voter, _electionService, _votingService).Show();
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
