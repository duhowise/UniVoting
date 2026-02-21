using System;
using Avalonia.Controls;
using Avalonia.Threading;
using UniVoting.LiveView.ViewModels;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    public partial class TileControlLarge : UserControl
    {
        private readonly TileViewModel _viewModel;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public TileControlLarge()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public TileControlLarge(ILiveViewService liveViewService, ILogger logger)
        {
            _viewModel = new TileViewModel(liveViewService, logger, isSkipped: false);
            DataContext = _viewModel;
            InitializeComponent();
        }

        public void Initialize(string position)
        {
            _viewModel.Initialize(position);
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += async (_, _) => await _viewModel.UpdateCountAsync();
            timer.Start();
        }
    }
}
