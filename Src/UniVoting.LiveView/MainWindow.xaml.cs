using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.LiveView.ViewModels;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    public partial class MainWindow : Window
    {
        private readonly LiveViewMainWindowViewModel _viewModel;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public MainWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public MainWindow(ILiveViewService liveViewService, ILogger logger, IServiceProvider sp)
        {
            _sp = sp;
            _viewModel = new LiveViewMainWindowViewModel(liveViewService, logger);
            _viewModel.PositionsLoaded += positions =>
            {
                foreach (var position in positions)
                {
                    var large = _sp.GetRequiredService<TileControlLarge>();
                    large.Initialize(position.PositionName?.Trim() ?? string.Empty);
                    CastedVotesHolder.Children.Add(large);

                    var small = _sp.GetRequiredService<TileControlSmall>();
                    small.Initialize(position.PositionName?.Trim() ?? string.Empty);
                    SkippedVotesHolder.Children.Add(small);
                }
            };
            DataContext = _viewModel;
            InitializeComponent();
            Loaded += async (_, _) => await _viewModel.LoadAsync();
        }
    }
}
