using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using UniVoting.Model;
using UniVoting.Services;
using static System.Diagnostics.Process;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for ClientVoteCompletedPage.xaml
    /// </summary>
    public partial class ClientVoteCompletedPage
    {
       private int count;
        public ClientVoteCompletedPage(List<Vote> votes)
        {
            InitializeComponent();
            IgnoreTaskbarOnMaximize = true;
            VotingService.CastVote(votes);
            var _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 2);
            _timer.Tick += _timer_Tick;
            _timer.Start();
            count = 0;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            count++;
            RestartApplication();
        }
        public void RestartApplication()
        {
            if (count == 2)
            {
                this.Hide();
                if (Application.ResourceAssembly != null) Start(Application.ResourceAssembly.Location);
                if (Application.Current != null)Application.Current.Shutdown();
                // OnRestartDue(this);

            }


        }

        
    }
}
