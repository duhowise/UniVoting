using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using UniVoting.Model;
using UniVoting.Services;

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

        private async void ClientVoteCompletedPage_Loaded(object? sender, RoutedEventArgs e)
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
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 3);
            timer.Tick += _timer_Tick;
            timer.Start();
        }

        private void _timer_Tick(object? sender, EventArgs e)
        {
            _count++;
            RestartApplication();
        }

        public void RestartApplication()
        {
            if (_count == 1)
            {
                Close();
                var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                if (!string.IsNullOrEmpty(exePath))
                    Process.Start(exePath);
                (Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Shutdown();
            }
        }
    }
}
