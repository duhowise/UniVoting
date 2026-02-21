using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
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
        readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public MainWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public MainWindow(ILiveViewService liveViewService, ILogger logger, IServiceProvider sp)
        {
            _liveViewService = liveViewService;
            _logger = logger;
            _sp = sp;
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
                    var large = _sp.GetRequiredService<TileControlLarge>();
                    large.Initialize(position.PositionName?.Trim() ?? string.Empty);
                    CastedVotesHolder.Children.Add(large);

                    var small = _sp.GetRequiredService<TileControlSmall>();
                    small.Initialize(position.PositionName?.Trim() ?? string.Empty);
                    SkippedVotesHolder.Children.Add(small);
                }
            }
        }
    }
}
