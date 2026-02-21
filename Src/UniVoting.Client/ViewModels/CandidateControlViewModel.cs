using System;
using System.Collections.Concurrent;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client.ViewModels;

public partial class CandidateControlViewModel : ObservableObject
{
    private readonly ConcurrentBag<Vote> _votes;
    private readonly Position _position;
    private readonly Voter _voter;

    public static event Action? VoteCast;

    public event Action? ShowConfirmDialog;
    public event Action? CloseDialog;

    [ObservableProperty]
    private int _candidateId;

    [ObservableProperty]
    private string? _candidateName;

    [ObservableProperty]
    private Bitmap? _candidateImage;

    [ObservableProperty]
    private string? _rankText;

    public Candidate Candidate { get; }

    public CandidateControlViewModel(IClientSessionService session)
    {
        _votes = session.Votes;
        _position = session.CurrentPosition ?? new Position();
        _voter = session.CurrentVoter ?? new Voter();
        Candidate = session.CurrentCandidate ?? new Candidate();

        CandidateId = Candidate.Id;
        CandidateName = Candidate.CandidateName?.ToUpper();
        RankText = $"#{Candidate.RankId}";
        if (Candidate.CandidatePicture != null)
            CandidateImage = Util.ByteToImageSource(Candidate.CandidatePicture);
    }

    [RelayCommand]
    private void Vote() => ShowConfirmDialog?.Invoke();

    public void ConfirmVote()
    {
        _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
        VoteCast?.Invoke();
        CloseDialog?.Invoke();
    }

    public void CancelVote() => CloseDialog?.Invoke();
}
