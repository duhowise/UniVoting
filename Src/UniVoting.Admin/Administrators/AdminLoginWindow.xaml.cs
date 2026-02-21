using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using UniVoting.ViewModels;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminLoginWindow : Window
    {
        private readonly AdminLoginViewModel _viewModel;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminLoginWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminLoginWindow(IElectionConfigurationService electionService, IVotingService votingService, ILogger logger, IServiceProvider sp, IAdminSessionService session)
        {
            _sp = sp;
            _viewModel = new AdminLoginViewModel(electionService, session);
            _viewModel.LoginSucceeded += () =>
            {
                _sp.GetRequiredService<MainWindow>().Show();
                Close();
            };
            _viewModel.ErrorOccurred += async msg =>
            {
                string title = msg.StartsWith("Could not connect") ? "Connection Error" : "Login Error";
                await MessageBoxManager.GetMessageBoxStandard(title, msg).ShowAsync();
            };
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
