using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;
using Wpf.Ui.Controls;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for ClientVotingPage.xaml
    /// </summary>
    public partial class ClientVotingPage : Page
    {
        private ConcurrentBag<Vote> _votes;
        private ConcurrentBag<SkippedVotes> _skippedVotes;
        private Position _position;
        private Voter _voter;
        public delegate void VoteCompletedEventHandler(object source, EventArgs args);
        public event VoteCompletedEventHandler VoteCompleted;

        private SkipVoteDialogControl skipVoteDialogControl;
        private FluentWindow _parentWindow;

        public ClientVotingPage(Voter voter, Position position, ConcurrentBag<Vote> votes, ConcurrentBag<SkippedVotes> skippedVotes)
        {
            InitializeComponent();
            this._voter = voter;
            this._position = position;
            _votes = votes;
            _skippedVotes = skippedVotes;
            BtnSkipVote.Click += BtnSkipVote_Click;
            Loaded += ClientVotingPage_Loaded;
            
            _parentWindow = Window.GetWindow(this) as FluentWindow;
            skipVoteDialogControl = new SkipVoteDialogControl(position);
            skipVoteDialogControl.BtnYes.Click += BtnYesClick;
            skipVoteDialogControl.BtnNo.Click += BtnNoClick;
        }


        private void ClientVotingPage_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxWelcome.Content = $"Welcome, {_voter.VoterName ?? string.Empty}";

            if (string.IsNullOrWhiteSpace(_position.Faculty) || _position.Faculty.Trim().Equals(_voter.Faculty.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                PositionName.Content = _position.PositionName.ToUpper();
                if (_position.Candidates.Count() == 1)
                {
                    BtnSkipVote.IsEnabled = false;
                    candidatesHolder.Children.Add(new YesOrNoCandidateControl(_votes, _position, _position.Candidates.FirstOrDefault(), _voter, _skippedVotes));
                }
                else
                {
                    BtnSkipVote.IsEnabled = true;
                    foreach (var candidate in _position.Candidates)
                    {
                        candidatesHolder.Children.Add(new CandidateControl(_votes, _position, candidate, _voter));
                    }
                }
            }
            else
            {
                OnVoteCompleted(this);
            }
        }

        private async void BtnSkipVote_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Skip Vote",
                Content = skipVoteDialogControl,
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };
            
            var result = await dialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                _skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
                OnVoteCompleted(this);
            }
        }

        private void BtnYesClick(object sender, RoutedEventArgs e)
        {
            // This will be handled by the ContentDialog Primary button
        }
        
        private void BtnNoClick(object sender, RoutedEventArgs e)
        {
            // This will be handled by the ContentDialog Secondary button
        }

        private void OnVoteCompleted(object source)
        {
            VoteCompleted?.Invoke(source, EventArgs.Empty);
        }
    }
}
