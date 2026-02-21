using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using SixLabors.ImageSharp;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminSetUpElectionPage : UserControl
    {
        private Avalonia.Media.Color _chosencolor;
        private readonly IElectionConfigurationService _electionService;

        public AdminSetUpElectionPage()
        {
            InitializeComponent();
            _electionService = null!;
        }

        public AdminSetUpElectionPage(IElectionConfigurationService electionService)
        {
            _electionService = electionService;
            InitializeComponent();
            BtnUploadImage.Click += BtnUploadImage_Click;
            Colorbox.GotFocus += Colorbox_GotFocus;
            SaveElection.Click += SaveElection_Click;
        }

        private void Colorbox_GotFocus(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var colorText = Colorbox.Text?.Trim() ?? string.Empty;
            if (colorText.Contains(","))
            {
                var parts = colorText.Split(',');
                if (parts.Length == 3 &&
                    byte.TryParse(parts[0], out byte r) &&
                    byte.TryParse(parts[1], out byte g) &&
                    byte.TryParse(parts[2], out byte b))
                {
                    _chosencolor = Avalonia.Media.Color.FromRgb(r, g, b);
                    ColoView.Fill = new SolidColorBrush(_chosencolor);
                }
            }
        }

        private async void SaveElection_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxElectionName.Text) || !string.IsNullOrWhiteSpace(TextBoxSubtitle.Text))
            {
                await _electionService.NewElection(new Setting
                {
                    ElectionName = TextBoxElectionName.Text,
                    EletionSubTitle = TextBoxSubtitle.Text,
                    Colour = string.Join(",", _chosencolor.R.ToString(), _chosencolor.G.ToString(), _chosencolor.B.ToString()),
                    Logo = Util.ConvertToBytes(Logo)
                });
                Util.Clear(this);
            }
        }

        private async void BtnUploadImage_Click(object? sender, RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);
            if (topLevel == null) return;
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Select a picture",
                AllowMultiple = false,
                FileTypeFilter = new[] { new FilePickerFileType("Images") { Patterns = new[] { "*.jpg", "*.jpeg", "*.png" } } }
            });
            if (files.Count > 0)
            {
                var filePath = files[0].Path.LocalPath;
                using var img = SixLabors.ImageSharp.Image.Load(filePath);
                var resized = Util.ResizeImage(img, 300, 300);
                Logo.Source = Util.BitmapToImageSource(resized);
            }
        }
    }
}
