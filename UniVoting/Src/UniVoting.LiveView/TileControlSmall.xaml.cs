using System;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Threading;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for TileControlSmall.xaml
    /// </summary>
    public partial class TileControlSmall : UserControl
    {
        private ILogger _logger;
        private DispatcherTimer _timer;
        private string _position;
        public TileControlSmall(String position)
        {
            InitializeComponent();
           _logger=new SystemEventLoggerService();
            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 1)
            };
            _timer.Tick += _timer_Tick;
            _timer.Start();
            _position = position;
            Position.Text = _position.ToUpper();
        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                VoteCount.Text = $"{await LiveViewService.VotesSkipppedCountAsync(_position)}";
            }
            catch (SqlException exception)
            {
                _logger.Log(exception);

            }
            catch (Exception exception)
            {
                _logger.Log(exception);
            }
            //finally
            //{
            //    VoteCount.Text = $"{await LiveViewService.VotesSkipppedCountAsync(_position)}";
            //}

        }
    }
}
