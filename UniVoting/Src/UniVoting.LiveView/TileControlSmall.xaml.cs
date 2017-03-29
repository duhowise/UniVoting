using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            this.Tile.Title = position;
            _position = position;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            VoteCount.Content = $"{LiveViewService.VotesSkipppedCount(_position)}";
            
        }
    }
}
