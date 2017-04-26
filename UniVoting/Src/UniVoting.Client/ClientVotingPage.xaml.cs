using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for ClientVotingPage.xaml
    /// </summary>
    public partial class ClientVotingPage : Page
    {
        private List<Vote> _votes;
        private Position _position;
        private Voter _voter;
        public delegate void VoteCompletedEventHandler(object source, EventArgs args);
        public event VoteCompletedEventHandler VoteCompleted;
        public ClientVotingPage(Voter voter, Position position, List<Vote> votes)
        {
            InitializeComponent();
            this._voter = voter;
            this._position = position;
            _votes = votes;
            BtnSkipVote.Click += BtnSkipVote_Click;
            Loaded += ClientVotingPage_Loaded;
           
            foreach (var candidate in _position.Candidates)
            {
                candidatesHolder.Children.Add(new CandidateControl(_votes, _position,candidate, _voter));

            }

        }
      
        private void ClientVotingPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PositionName.Content = _position.PositionName.ToUpper();
            TextBoxWelcome.Content = $"Welcome, {_voter.VoterName ?? String.Empty} !";
        }

        private async void BtnSkipVote_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var metroWindow = (Window.GetWindow(this) as MetroWindow);
            var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 18, AffirmativeButtonText = "Ok" };

            MessageDialogResult result = await metroWindow.ShowMessageAsync("Skip Vote", $"Are You Sure You Want to Skip {_position.PositionName} ?", MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
            if (result == MessageDialogResult.Affirmative)
            {
                VotingService.SkipVote(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
                OnVoteCompleted(this);

            }
           
        }

        protected virtual void OnVoteCompleted(object source)
        {
            VoteCompleted?.Invoke(source, EventArgs.Empty);
        }
    }
}
