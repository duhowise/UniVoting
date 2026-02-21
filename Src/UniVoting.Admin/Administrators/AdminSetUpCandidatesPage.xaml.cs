using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Media.Imaging;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminSetUpCandidatesPage : UserControl
    {
        internal class CandidateDto
        {
            public CandidateDto() { Id = 0; }
            public CandidateDto(int id, int positionid, string candidateName, Bitmap? candidatepicture, int rankId, string position)
            {
                Id = id;
                Positionid = positionid;
                CandidateName = candidateName;
                CandidatePicture = candidatepicture;
                RankId = rankId;
                Position = position;
            }
            public int Id { get; set; }
            public int Positionid { get; }
            public int? PositionId { get; set; }
            public string? CandidateName { get; set; }
            public Bitmap? CandidatePicture { get; set; }
            public int? RankId { get; set; }
            public string? Position { get; set; }
            public string Rank => $"Rank: {RankId}";
        }

        internal ObservableCollection<CandidateDto> Candidates = new ObservableCollection<CandidateDto>();
        private List<int> _rank;
        private int _candidateId;

        public AdminSetUpCandidatesPage()
        {
            _candidateId = 0;
            InitializeComponent();
            SaveCandidate.Click += SaveCandidate_Click;
            _rank = new List<int>();
            for (int i = 1; i <= 10; ++i) _rank.Add(i);
            RankCombo.ItemsSource = _rank;
            Loaded += Page_Loaded;
            CandidatesList.DoubleTapped += CandidatesList_OnDoubleTapped;
        }

        private async void SaveCandidate_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CandidateName.Text) && PositionCombo.SelectedItem != null)
            {
                var position = PositionCombo.SelectedItem as Model.Position;
                var rankVal = RankCombo.SelectedItem is int rv ? rv : 1;
                var candidate = new Candidate
                {
                    Id = _candidateId,
                    CandidateName = CandidateName.Text,
                    CandidatePicture = Util.ConvertToBytes(CandidateImage),
                    PositionId = position?.Id ?? 0,
                    RankId = rankVal
                };
                await ElectionConfigurationService.SaveCandidate(candidate);
                Util.Clear(this);
                PositionCombo.ItemsSource = await ElectionConfigurationService.GetAllPositionsAsync();
                await RefreshCandidateList();
            }
        }

        private async void Page_Loaded(object? sender, RoutedEventArgs e)
        {
            PositionCombo.ItemsSource = await ElectionConfigurationService.GetAllPositionsAsync();
            await RefreshCandidateList();
        }

        private async System.Threading.Tasks.Task RefreshCandidateList()
        {
            Candidates.Clear();
            var candidates = await new ElectionConfigurationService().GetCandidateWithDetails();
            foreach (var candidate in candidates)
            {
                var newcandidate = new CandidateDto(
                    candidate.Id, Convert.ToInt32(candidate.PositionId),
                    candidate.CandidateName,
                    candidate.CandidatePicture != null ? Util.ByteToImageSource(candidate.CandidatePicture) : null,
                    Convert.ToInt32(candidate.RankId),
                    candidate.Position?.PositionName ?? string.Empty);
                Candidates.Add(newcandidate);
            }
            CandidatesList.ItemsSource = Candidates;
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
                CandidateImage.Source = Util.BitmapToImageSource(resized);
            }
        }

        private void CandidatesList_OnDoubleTapped(object? sender, TappedEventArgs e)
        {
            var editCandidate = CandidatesList.SelectedItem as CandidateDto;
            if (editCandidate != null)
            {
                _candidateId = editCandidate.Id;
                CandidateName.Text = editCandidate.CandidateName;
                CandidateImage.Source = editCandidate.CandidatePicture;
                if (editCandidate.PositionId.HasValue)
                    PositionCombo.SelectedItem = Candidates.Count > 0 ? null : null; // Will be set when positions load
                RankCombo.SelectedValue = editCandidate.RankId;
            }
        }
    }
}
