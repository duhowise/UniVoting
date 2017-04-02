using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using MahApps.Metro.Controls;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for ClientVoteCompletedPage.xaml
    /// </summary>
    public partial class ClientVoteCompletedPage : Page
    {
        public delegate void RestartDueEventHandler(object source, EventArgs args);
        public static event RestartDueEventHandler RestartDue;
        private int count;
        public ClientVoteCompletedPage()
        {
            InitializeComponent();
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
                var metroWindow = (Window.GetWindow(this) as MetroWindow);
                metroWindow?.Hide();
                OnRestartDue(this);

            }


        }

        protected static void OnRestartDue(object source)
        {
            RestartDue?.Invoke(source, EventArgs.Empty);
        }
    }
}
