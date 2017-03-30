using System;
using System.Collections.Generic;
using System.Windows.Controls;
using UniVoting.Model;

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
        public ClientVotingPage(Voter voter, Position position)
        {
            InitializeComponent();
            this._voter = voter;
            this._position = position;
            BtnSkipVote.Click += BtnSkipVote_Click;
            Loaded += ClientVotingPage_Loaded;
            _votes = new List<Vote>();

            foreach (var candidate in _position.Candidates)
            {
                this.candidatesHolder.Children.Add(new CandidateControl(_votes, _position,candidate, _voter));

            }

        }
        public delegate void VoteCompletedEventHandler (object source, EventArgs args);

        public event VoteCompletedEventHandler VoteCompleted;
        private void ClientVotingPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PositionName.Content = _position.PositionName;
            TextBoxWelcome.Content = _voter.VoterName ?? String.Empty;
        }

        private void BtnSkipVote_Click(object sender, System.Windows.RoutedEventArgs e)
        {
             OnVoteCompleted(this);
        }

        protected virtual void OnVoteCompleted(object source)
        {
            VoteCompleted?.Invoke(source, EventArgs.Empty);
        }
    }
}
