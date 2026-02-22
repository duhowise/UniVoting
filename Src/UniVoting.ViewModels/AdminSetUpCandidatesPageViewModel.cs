using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SixLabors.ImageSharp.Processing;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class AdminSetUpCandidatesPageViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;
    private int _candidateId;

    public event Func<Task<string?>>? PickImageRequested;

    [ObservableProperty]
    private ObservableCollection<CandidateDisplayItem> _candidates = new();

    [ObservableProperty]
    private ObservableCollection<Position> _positions = new();

    [ObservableProperty]
    private List<int> _ranks = new();

    [ObservableProperty]
    private string? _candidateName;

    [ObservableProperty]
    private Position? _selectedPosition;

    [ObservableProperty]
    private int _selectedRank;

    [ObservableProperty]
    private Bitmap? _candidateImage;

    [ObservableProperty]
    private byte[] _candidateImageBytes = Array.Empty<byte>();

    public AdminSetUpCandidatesPageViewModel(IElectionConfigurationService electionService)
    {
        _electionService = electionService;
        _candidateId = 0;
        var rankList = new List<int>();
        for (int i = 1; i <= 10; i++) rankList.Add(i);
        Ranks = rankList;
    }

    public async Task LoadAsync()
    {
        var positionList = await _electionService.GetAllPositionsAsync();
        Positions = new ObservableCollection<Position>(positionList);
        await RefreshCandidatesAsync();
    }

    private async Task RefreshCandidatesAsync()
    {
        Candidates.Clear();
        var candidates = await _electionService.GetCandidateWithDetails();
        foreach (var c in candidates)
        {
            Candidates.Add(new CandidateDisplayItem
            {
                Id = c.Id,
                PositionId = c.PositionId ?? 0,
                CandidateName = c.CandidateName,
                CandidatePicture = c.CandidatePicture != null ? ByteToBitmap(c.CandidatePicture) : null,
                PictureBytes = c.CandidatePicture ?? Array.Empty<byte>(),
                RankId = (int)(c.RankId ?? 1),
                PositionName = c.Position?.PositionName ?? string.Empty
            });
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
                CandidateImageBytes = ms.ToArray();
                CandidateImage = new Bitmap(new System.IO.MemoryStream(CandidateImageBytes));
            }
        }
    }

    [RelayCommand]
    private async Task SaveCandidateAsync()
    {
        if (!string.IsNullOrWhiteSpace(CandidateName) && SelectedPosition != null)
        {
            var candidate = new Candidate
            {
                Id = _candidateId,
                CandidateName = CandidateName,
                CandidatePicture = CandidateImageBytes,
                PositionId = SelectedPosition.Id,
                RankId = SelectedRank
            };
            await _electionService.SaveCandidate(candidate);
            ClearForm();
            var positionList = await _electionService.GetAllPositionsAsync();
            Positions = new ObservableCollection<Position>(positionList);
            await RefreshCandidatesAsync();
        }
    }

    public void SelectCandidate(CandidateDisplayItem item)
    {
        _candidateId = item.Id;
        CandidateName = item.CandidateName;
        CandidateImage = item.CandidatePicture;
        CandidateImageBytes = item.PictureBytes ?? Array.Empty<byte>();
        SelectedRank = item.RankId;
        SelectedPosition = Positions.FirstOrDefault(p => p.Id == item.PositionId);
    }

    private void ClearForm()
    {
        _candidateId = 0;
        CandidateName = string.Empty;
        CandidateImage = null;
        CandidateImageBytes = Array.Empty<byte>();
        SelectedPosition = null;
        SelectedRank = 1;
    }

    private static Bitmap ByteToBitmap(byte[] bytes)
    {
        using var ms = new System.IO.MemoryStream(bytes);
        return new Bitmap(ms);
    }

    public class CandidateDisplayItem
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public string? CandidateName { get; set; }
        public Bitmap? CandidatePicture { get; set; }
        public byte[]? PictureBytes { get; set; }
        public int RankId { get; set; }
        public string? PositionName { get; set; }
        public string Rank => $"Rank: {RankId}";
    }
}
