using System;
using Avalonia.Controls;
using MsBox.Avalonia;
using UniVoting.ViewModels;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminCreateAccountPage : UserControl
    {
        private readonly AdminCreateAccountPageViewModel _viewModel;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminCreateAccountPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminCreateAccountPage(IElectionConfigurationService electionService)
        {
            _viewModel = new AdminCreateAccountPageViewModel(electionService);
            _viewModel.SuccessOccurred += async msg =>
                await MessageBoxManager.GetMessageBoxStandard("Success!", msg).ShowAsync();
            _viewModel.ErrorOccurred += async msg =>
                await MessageBoxManager.GetMessageBoxStandard("Wait!", msg).ShowAsync();
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
