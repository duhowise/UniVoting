using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class ClientVoteCompletedViewModel : ObservableObject
{
    private readonly IVotingService _votingService;
    private readonly IElectionConfigurationService _electionService;
    private readonly ConcurrentBag<Vote> _votes;
    private readonly ConcurrentBag<SkippedVotes> _skippedVotes;
    private readonly Voter _voter;

    public event Action<byte[]>? BackgroundImageLoaded;

    [ObservableProperty]
    private string _thankYouText = "Thank You For Voting";

    public ClientVoteCompletedViewModel(
        IClientSessionService session,
        IVotingService votingService,
        IElectionConfigurationService electionService)
    {
        _votes = session.Votes;
        _voter = session.CurrentVoter!;
        _skippedVotes = session.SkippedVotes;
        _votingService = votingService;
        _electionService = electionService;
    }

    public async Task LoadAsync()
    {
        try
        {
            var election = _electionService.ConfigureElection();
            if (election?.Logo != null)
                BackgroundImageLoaded?.Invoke(election.Logo);
            await _votingService.CastVote(_votes, _voter, _skippedVotes);
            ThankYouText = $"Good Bye {_voter.VoterName?.ToUpper()}, Thank You For Voting";
        }
        catch
        {
            ThankYouText = "Sorry An Error Occurred.\nYour Votes Were not Submitted.\nContact the Administrators";
            await _votingService.ResetVoter(_voter);
        }
    }
}
