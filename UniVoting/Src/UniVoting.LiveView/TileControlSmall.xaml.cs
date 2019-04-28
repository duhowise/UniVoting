using System;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Threading;
using UniVoting.Core;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    /// <inheritdoc cref="UserControl" />
    /// <summary>
    /// Interaction logic for TileControlSmall.xaml
    /// </summary>
    public partial class TileControlSmall : UserControl
    {
        private readonly Position _position;
        private readonly ILiveViewService _liveViewService;
        //  private readonly ILogger _logger;
        public TileControlSmall(Position position,ILiveViewService liveViewService)
        {
            _position = position;
            _liveViewService = liveViewService;
            InitializeComponent();
            //  _logger=new SystemEventLoggerService();
            var timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 1)
            };
            timer.Tick += _timer_Tick;
            timer.Start();
            Position.Text = _position.PositionName.ToUpper();
        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                VoteCount.Text = $"{await _liveViewService.VotesSkipppedCountAsync(_position.Id)}";
            }
            catch (SqlException)
            {
                //  _logger.Log(exception);

            }
            catch (Exception )
            {
                //  _logger.Log(exception);
            }
            //finally
            //{
            //    VoteCount.Text = $"{await LiveViewService.VotesSkipppedCountAsync(_position)}";
            //}

        }
    }
}
