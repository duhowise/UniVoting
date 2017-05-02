using System;
using System.Data.SqlClient;
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
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
            _position = position;
            Position.Text = _position;
        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                VoteCount.Text = $"{await LiveViewService.VotesSkipppedCountAsync(_position)}";
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception);
                //show connection Error
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
               // throw;
            }
            finally
            {
                VoteCount.Text = $"{await LiveViewService.VotesSkipppedCountAsync(_position)}";
            }

        }
    }
}
