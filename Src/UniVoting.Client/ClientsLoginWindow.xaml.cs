using System;
using Avalonia.Controls;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using UniVoting.ViewModels;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client
{
    public partial class ClientsLoginWindow : Window
    {
        private readonly ClientLoginViewModel _viewModel;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public ClientsLoginWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public ClientsLoginWindow(IElectionConfigurationService electionService, IVotingService votingService, ILogger logger, IServiceProvider sp, IClientSessionService session)
        {
            _sp = sp;
            _viewModel = new ClientLoginViewModel(electionService, session);
            _viewModel.LoginSucceeded += () =>
            {
                _sp.GetRequiredService<MainWindow>().Show();
                Hide();
            };
            _viewModel.ErrorOccurred += async (title, msg) =>
                await MessageBoxManager.GetMessageBoxStandard(title, msg).ShowAsync();
            _viewModel.BackgroundImageLoaded += bytes =>
                MainGrid.Background = new ImageBrush(Util.BytesToBitmapImage(bytes)) { Opacity = 0.2 };
            DataContext = _viewModel;
            InitializeComponent();
            Loaded += (_, _) => _viewModel.Initialize();
        }

        protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
