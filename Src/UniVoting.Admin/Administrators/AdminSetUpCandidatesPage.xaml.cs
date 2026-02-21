using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using UniVoting.Admin.ViewModels;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminSetUpCandidatesPage : UserControl
    {
        private readonly AdminSetUpCandidatesPageViewModel _viewModel;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminSetUpCandidatesPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminSetUpCandidatesPage(IElectionConfigurationService electionService)
        {
            _viewModel = new AdminSetUpCandidatesPageViewModel(electionService);
            _viewModel.PickImageRequested += PickImageAsync;
            DataContext = _viewModel;
            InitializeComponent();
            SaveCandidate.Click += async (_, _) => await _viewModel.SaveCandidateCommand.ExecuteAsync(null);
            CandidatesList.DoubleTapped += CandidatesList_OnDoubleTapped;
            Loaded += async (_, _) => await _viewModel.LoadAsync();
        }

        private async Task<string?> PickImageAsync()
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null) return null;
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select a picture",
                AllowMultiple = false,
                FileTypeFilter = new[] { new FilePickerFileType("Images") { Patterns = new[] { "*.jpg", "*.jpeg", "*.png" } } }
            });
            return files.Count > 0 ? files[0].Path.LocalPath : null;
        }

        private void CandidatesList_OnDoubleTapped(object? sender, TappedEventArgs e)
        {
            if (CandidatesList.SelectedItem is AdminSetUpCandidatesPageViewModel.CandidateDisplayItem item)
                _viewModel.SelectCandidate(item);
        }
    }
}
