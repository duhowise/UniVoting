using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using UniVoting.Admin.ViewModels;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminSetUpElectionPage : UserControl
    {
        private readonly AdminSetUpElectionPageViewModel _viewModel;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminSetUpElectionPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminSetUpElectionPage(IElectionConfigurationService electionService)
        {
            _viewModel = new AdminSetUpElectionPageViewModel(electionService);
            _viewModel.PickImageRequested += PickImageAsync;
            DataContext = _viewModel;
            InitializeComponent();
            Colorbox.GotFocus += Colorbox_GotFocus;
        }

        private void Colorbox_GotFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            _viewModel.UpdateColourPreview(Colorbox.Text);
            ColoView.Fill = new SolidColorBrush(_viewModel.ChosenColor);
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
    }
}
