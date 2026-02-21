using System;
using Avalonia.Controls;
using Avalonia.Threading;
using Microsoft.Data.SqlClient;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    public partial class TileControlSmall : UserControl
    {
        private readonly ILogger _logger;
        private readonly string _position;

        public TileControlSmall(string position)
        {
            InitializeComponent();
            _logger = new SystemEventLoggerService();
            var timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1) };
            timer.Tick += _timer_Tick;
            timer.Start();
            _position = position.Trim();
            Position.Text = _position.ToUpper();
        }

        private async void _timer_Tick(object? sender, EventArgs e)
        {
            try
            {
                VoteCount.Text = $"{await LiveViewService.VotesSkipppedCountAsync(_position.Trim())}";
            }
            catch (SqlException exception) { _logger.Log(exception); }
            catch (Exception exception) { _logger.Log(exception); }
        }
    }
}
