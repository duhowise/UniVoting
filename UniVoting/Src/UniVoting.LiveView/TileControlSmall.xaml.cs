using System;
using System.Windows.Controls;
using System.Windows.Threading;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for TileControlSmall.xaml
    /// </summary>
    public partial class TileControlSmall : UserControl
    {
        private DispatcherTimer _timer;
        private string _position;
        public TileControlSmall(String position)
        {
            InitializeComponent();
           
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 2);
            _timer.Tick += _timer_Tick;
            _timer.Start();
            _position = position;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
           // VoteCount.Content = $"{LiveViewService.VotesSkipppedCount(_position)}";
            
        }
    }
}
