using System;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Threading;
using UniVoting.Core;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for TileControlLarge.xaml
    /// </summary>
    public partial class TileControlLarge : UserControl
    {
        private readonly Position _position;
        private readonly ILiveViewService _liveViewService;
        //private readonly ILogger _logger;
        public TileControlLarge(Position position,ILiveViewService liveViewService)
        {
            _position = position;
            _liveViewService = liveViewService;

            InitializeComponent();
            var timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 1)};
            timer.Tick += _timer_Tick;
            timer.Start();
            PositionName.Text = _position.PositionName.ToUpperInvariant();

        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                VoteCount.Text = $"{await _liveViewService.VoteCountAsync(_position.Id)}";
            }
            catch (SqlException )
            {
                //_logger.Log(exception);
            }
            catch (Exception )
            {
                //  _logger.Log(exception);
            }
            //finally
            //{
            //    VoteCount.Text = $"{await LiveViewService.VoteCountAsync(_position)}";
            //}
        }
    }
}
