using System;
using Avalonia.Controls;
using Avalonia.Input;
using MsBox.Avalonia;
using UniVoting.ViewModels;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminDispensePasswordWindow : Window
    {
        private readonly AdminDispensePasswordViewModel _viewModel;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminDispensePasswordWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminDispensePasswordWindow(IElectionConfigurationService electionService)
        {
            _viewModel = new AdminDispensePasswordViewModel(electionService);
            _viewModel.ShowVoterInfo += async (title, msg) =>
                await MessageBoxManager.GetMessageBoxStandard(title, msg).ShowAsync();
            DataContext = _viewModel;
            InitializeComponent();
            StudentsSearchList.DoubleTapped += StudentsSearchList_DoubleTapped;
            Loaded += async (_, _) => await _viewModel.LoadAsync();
        }

        private async void StudentsSearchList_DoubleTapped(object? sender, TappedEventArgs e)
        {
            var student = StudentsSearchList.SelectedItem as Voter;
            _viewModel.ShowSelectedVoterInfo(student);
            _viewModel.SearchTerm = string.Empty;
        }
    }
}
