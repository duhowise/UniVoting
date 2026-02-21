using System;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SixLabors.ImageSharp.Processing;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.ViewModels;

public partial class AdminSetUpElectionPageViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;

    public event Func<Task<string?>>? PickImageRequested;
    public event Action<string>? ErrorOccurred;
    public event Action<string>? SuccessOccurred;

    [ObservableProperty]
    private string? _electionName;

    [ObservableProperty]
    private string? _subtitle;

    [ObservableProperty]
    private string? _colourText;

    [ObservableProperty]
    private Bitmap? _logo;

    [ObservableProperty]
    private byte[] _logoBytes = Array.Empty<byte>();

    [ObservableProperty]
    private Color _chosenColor;

    [ObservableProperty]
    private IBrush _chosenColorBrush = Brushes.Transparent;

    public AdminSetUpElectionPageViewModel(IElectionConfigurationService electionService)
    {
        _electionService = electionService;
    }

    partial void OnColourTextChanged(string? value) => UpdateColourPreview(value);

    public void UpdateColourPreview(string? colourText)
    {
        if (!string.IsNullOrWhiteSpace(colourText) && colourText.Contains(","))
        {
            var parts = colourText.Split(',');
            if (parts.Length == 3 &&
                byte.TryParse(parts[0].Trim(), out byte r) &&
                byte.TryParse(parts[1].Trim(), out byte g) &&
                byte.TryParse(parts[2].Trim(), out byte b))
            {
                ChosenColor = Color.FromRgb(r, g, b);
                ChosenColorBrush = new SolidColorBrush(ChosenColor);
            }
        }
    }

    [RelayCommand]
    private async Task UploadImageAsync()
    {
        if (PickImageRequested != null)
        {
            var filePath = await PickImageRequested.Invoke();
            if (!string.IsNullOrEmpty(filePath))
            {
                using var img = SixLabors.ImageSharp.Image.Load(filePath);
                img.Mutate(x => x.Resize(300, 300));
                using var ms = new System.IO.MemoryStream();
                img.Save(ms, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                ms.Position = 0;
                Logo = new Bitmap(ms);
                LogoBytes = ms.ToArray();
            }
        }
    }

    [RelayCommand]
    private async Task SaveElectionAsync()
    {
        if (!string.IsNullOrWhiteSpace(ElectionName) && !string.IsNullOrWhiteSpace(Subtitle))
        {
            try
            {
                await _electionService.NewElection(new Setting
                {
                    ElectionName = ElectionName,
                    EletionSubTitle = Subtitle,
                    Colour = string.Join(",", ChosenColor.R, ChosenColor.G, ChosenColor.B),
                    Logo = LogoBytes
                });
                ElectionName = string.Empty;
                Subtitle = string.Empty;
                ColourText = string.Empty;
                Logo = null;
                LogoBytes = Array.Empty<byte>();
                ChosenColorBrush = Brushes.Transparent;
                SuccessOccurred?.Invoke("Election saved successfully.");
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke($"Failed to save election: {ex.Message}");
            }
        }
    }
}
