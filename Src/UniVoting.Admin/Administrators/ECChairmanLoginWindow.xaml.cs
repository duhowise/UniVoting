using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using UniVoting.Admin.ViewModels;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class EcChairmanLoginWindow : Window
    {
        private readonly EcChairmanLoginViewModel _viewModel;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public EcChairmanLoginWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public EcChairmanLoginWindow(IElectionConfigurationService electionService, IServiceProvider sp)
        {
            _sp = sp;
            _viewModel = new EcChairmanLoginViewModel(electionService);
            _viewModel.LoginSucceeded += () =>
            {
                _sp.GetRequiredService<ReportViewerWindow>().Show();
                Close();
            };
            _viewModel.ErrorOccurred += async msg =>
                await MessageBoxManager.GetMessageBoxStandard("Login Error", msg).ShowAsync();
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
