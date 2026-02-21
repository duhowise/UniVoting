using System;
using Microsoft.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Threading;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for TileControlLarge.xaml
    /// </summary>
    public partial class TileControlLarge : UserControl
    {
        private readonly ILogger _logger;
        private readonly string _position;
        public TileControlLarge(string position)
        {
            InitializeComponent();
            _logger=new SystemEventLoggerService();
            var timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 1)};
            timer.Tick += _timer_Tick;
            timer.Start();
            _position = position.Trim();
            Position.Text = _position.ToUpper();

        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                VoteCount.Text = $"{await LiveViewService.VoteCountAsync(_position)}";
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
            //    VoteCount.Text = $"{await LiveViewService.VoteCountAsync(_position)}";
            //}
        }
    }
}
