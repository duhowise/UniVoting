using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class ConfirmDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _confirmText;

    [ObservableProperty]
    private Bitmap? _candidateImage;

    public ConfirmDialogViewModel(IClientSessionService session)
    {
        var candidate = session.CurrentCandidate ?? new Candidate();
        ConfirmText = $"Are you sure you want to vote for {candidate.CandidateName}";
        if (candidate.CandidatePicture != null)
            CandidateImage = Util.ByteToImageSource(candidate.CandidatePicture);
    }
}
