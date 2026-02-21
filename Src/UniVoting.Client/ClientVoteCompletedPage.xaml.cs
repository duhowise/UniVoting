using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using UniVoting.Model;
using UniVoting.Services;
using static System.Diagnostics.Process;

namespace UniVoting.Client
{
    public partial class ClientVoteCompletedPage : Window
    {
        private ConcurrentBag<Vote> _votes;
        private Voter _voter;
        private ConcurrentBag<SkippedVotes> _skippedVotes;
        private int _count;

        public ClientVoteCompletedPage(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVotes> skippedVotes)
        {
            _votes = votes;
            _voter = voter;
            _skippedVotes = skippedVotes;
            InitializeComponent();
            _count = 0;
            Loaded += ClientVoteCompletedPage_Loaded;
        }

        private async void ClientVoteCompletedPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var election = ElectionConfigurationService.ConfigureElection();
                if (election?.Logo != null)
                    MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(election.Logo)) { Opacity = 0.2 };
                await VotingService.CastVote(_votes, _voter, _skippedVotes);
                Text.Text = $"Good Bye {_voter.VoterName?.ToUpper()}, Thank You For Voting";
            }
            catch (Exception)
            {
                Text.Text = "Sorry An Error Occurred.\nYour Votes Were not Submitted.\nContact the Administrators";
                await VotingService.ResetVoter(_voter);
            }
            var _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 3);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _count++;
            RestartApplication();
        }

        public void RestartApplication()
        {
            if (_count == 1)
            {
                this.Hide();
                Start(Application.ResourceAssembly.Location);
                if (Application.Current != null) Application.Current.Shutdown();
            }
        }
    }
}
