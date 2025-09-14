using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Controls;
using System.Windows.Threading;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for TileControlLarge.xaml
    /// </summary>
    public partial class TileControlLarge : UserControl
    {
        private readonly ILogger<TileControlLarge> _logger;
        private readonly LiveViewService _liveViewService;
        private DispatcherTimer _timer;
        private string _position;
        
        public TileControlLarge(String position, LiveViewService liveViewService, ILogger<TileControlLarge> logger)
        {
            InitializeComponent();
            _logger = logger;
            _liveViewService = liveViewService;
            
            _timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 1)};
            _timer.Tick += _timer_Tick;
            _timer.Start();
            _position = position;
            Position.Text = _position.ToUpper();
        }

        private async void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                VoteCount.Text = $"{await _liveViewService.VoteCountAsync(_position)}";
            }
            catch (MySqlException exception)
            {
                _logger.LogError(exception, "MySQL exception occurred while getting vote count for position {Position}", _position);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "General exception occurred while getting vote count for position {Position}", _position);
            }
        }
    }
}
