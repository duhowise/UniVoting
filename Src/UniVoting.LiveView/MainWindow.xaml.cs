using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UniVoting.Model;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.LiveView
{
    public partial class MainWindow : Window
    {
        IEnumerable<Position> _positions;
        readonly ILiveViewService _liveViewService;
        readonly ILogger _logger;

        public MainWindow()
        {
            InitializeComponent();
            _positions = new List<Position>();
            _liveViewService = null!;
            _logger = null!;
        }

        public MainWindow(ILiveViewService liveViewService, ILogger logger)
        {
            _liveViewService = liveViewService;
            _logger = logger;
            InitializeComponent();
            _positions = new List<Position>();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object? sender, RoutedEventArgs e)
        {
            try
            {
                _positions = await _liveViewService.Positions();
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
            }
            finally
            {
                foreach (var position in _positions)
                {
                    CastedVotesHolder.Children.Add(new TileControlLarge(position.PositionName?.Trim() ?? string.Empty, _liveViewService, _logger));
                    SkippedVotesHolder.Children.Add(new TileControlSmall(position.PositionName?.Trim() ?? string.Empty, _liveViewService, _logger));
                }
            }
        }
    }
}
