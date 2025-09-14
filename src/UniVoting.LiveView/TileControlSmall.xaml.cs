using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
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
        private readonly ILogger<TileControlSmall> _logger;
        private readonly LiveViewService _liveViewService;
        private DispatcherTimer _timer;
        private string _position;
        
        public TileControlSmall(String position, LiveViewService liveViewService, ILogger<TileControlSmall> logger)
        {
            InitializeComponent();
            _logger = logger;
            _liveViewService = liveViewService;
           
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
                VoteCount.Text = $"{await _liveViewService.VotesSkipppedCountAsync(_position)}";
            }
            catch (MySqlException exception)
            {
                _logger.LogError(exception, "MySQL exception occurred while getting votes skipped count for position {Position}", _position);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "General exception occurred while getting votes skipped count for position {Position}", _position);
            }
        }
    }
}
