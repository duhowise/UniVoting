using System;
using System.Collections.Generic;
using System.Windows;
using UniVoting.Model;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.LiveView
{
    public partial class MainWindow : Window
    {
        IEnumerable<Position> _positions;
        readonly ILogger _logger;

        public MainWindow()
        {
            InitializeComponent();
            _positions = new List<Position>();
            _logger = new SystemEventLoggerService();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _positions = await LiveViewService.Positions();
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
            }
            finally
            {
                foreach (var position in _positions)
                {
                    CastedVotesHolder.Children.Add(new TileControlLarge(position.PositionName?.Trim()));
                    SkippedVotesHolder.Children.Add(new TileControlSmall(position.PositionName?.Trim()));
                }
            }
        }
    }
}
