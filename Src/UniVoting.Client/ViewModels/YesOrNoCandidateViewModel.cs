using System;
using System.Collections.Concurrent;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client.ViewModels;

public partial class YesOrNoCandidateViewModel : ObservableObject
{
    private readonly ConcurrentBag<Vote> _votes;
    private readonly ConcurrentBag<SkippedVotes> _skippedVotes;
    private readonly Position _position;
    private readonly Voter _voter;

    public static event Action? VoteCast;
    public static event Action? VoteNo;

    public event Action? ShowConfirmDialog;
    public event Action? ShowSkipDialog;
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

    public YesOrNoCandidateViewModel(IClientSessionService session)
    {
        _votes = session.Votes;
        _skippedVotes = session.SkippedVotes;
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
    private void VoteYes() => ShowConfirmDialog?.Invoke();

    [RelayCommand]
    private void VoteNoBtnClick() => ShowSkipDialog?.Invoke();

    public void ConfirmYesVote()
    {
        _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
        VoteCast?.Invoke();
        CloseDialog?.Invoke();
    }

    public void ConfirmSkip()
    {
        _skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
        VoteNo?.Invoke();
        CloseDialog?.Invoke();
    }

    public void CancelDialog() => CloseDialog?.Invoke();
}
