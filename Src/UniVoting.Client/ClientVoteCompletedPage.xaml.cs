using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Threading;
using UniVoting.Client.ViewModels;
using UniVoting.Services;

namespace UniVoting.Client
{
    public partial class ClientVoteCompletedPage : Window
    {
        private int _count;
        private readonly ClientVoteCompletedViewModel _viewModel;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public ClientVoteCompletedPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public ClientVoteCompletedPage(IClientSessionService session, IVotingService votingService, IElectionConfigurationService electionService)
        {
            _count = 0;
            _viewModel = new ClientVoteCompletedViewModel(session, votingService, electionService);
            _viewModel.BackgroundImageLoaded += bytes =>
                MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(bytes)) { Opacity = 0.2 };
            DataContext = _viewModel;
            InitializeComponent();
            Loaded += async (_, _) =>
            {
                await _viewModel.LoadAsync();
                var timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 3) };
                timer.Tick += TimerTick;
                timer.Start();
            };
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            _count++;
            if (_count == 1)
            {
                Close();
                var exePath = Environment.ProcessPath;
                if (!string.IsNullOrEmpty(exePath))
                    Process.Start(exePath);
                (Avalonia.Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.Shutdown();
            }
        }
    }
}
