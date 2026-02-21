using System;
using System.Collections.Concurrent;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client.ViewModels;

public partial class ClientVotingPageViewModel : ObservableObject
{
    private readonly IClientSessionService _session;
    private readonly ConcurrentBag<Vote> _votes;
    private readonly ConcurrentBag<SkippedVotes> _skippedVotes;

    public event Action? VoteCompleted;
    public event Action? ShowSkipDialog;

    [ObservableProperty]
    private string? _welcomeText;

    [ObservableProperty]
    private string? _positionName;

    [ObservableProperty]
    private bool _isSkipEnabled = true;

    public Position CurrentPosition { get; }
    public Voter CurrentVoter { get; }

    public ClientVotingPageViewModel(IClientSessionService session)
    {
        _session = session;
        _votes = session.Votes;
        _skippedVotes = session.SkippedVotes;
        CurrentPosition = session.CurrentPosition ?? new Position();
        CurrentVoter = session.CurrentVoter ?? new Voter();
    }

    public void Initialize()
    {
        WelcomeText = $"Welcome, {CurrentVoter.VoterName ?? string.Empty}";
        bool isFacultyMatch = string.IsNullOrWhiteSpace(CurrentPosition.Faculty) ||
            CurrentPosition.Faculty.Trim().Equals(CurrentVoter.Faculty?.Trim(), StringComparison.OrdinalIgnoreCase);

        if (isFacultyMatch)
        {
            PositionName = CurrentPosition.PositionName?.ToUpper();
        }
    }

    public void RecordSkip()
    {
        _skippedVotes.Add(new SkippedVotes { Positionid = CurrentPosition.Id, VoterId = CurrentVoter.Id });
        VoteCompleted?.Invoke();
    }

    [RelayCommand]
    private void SkipVote() => ShowSkipDialog?.Invoke();
}
