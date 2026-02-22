using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class ClientLoginViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;
    private readonly IClientSessionService _session;
    private Voter? _currentVoter;

    public event Action? LoginSucceeded;
    public event Action<string, string>? ErrorOccurred;
    public event Action<byte[]>? BackgroundImageLoaded;

    [ObservableProperty]
    private string? _pin;

    [ObservableProperty]
    private string? _votingName;

    [ObservableProperty]
    private string? _votingSubtitle;

    public ClientLoginViewModel(IElectionConfigurationService electionService, IClientSessionService session)
    {
        _electionService = electionService;
        _session = session;
    }

    public void Initialize()
    {
        try
        {
            var election = _electionService.ConfigureElection();
            if (election?.Logo != null)
                BackgroundImageLoaded?.Invoke(election.Logo);
            if (election != null)
            {
                VotingName = election.ElectionName?.ToUpper();
                VotingSubtitle = election.EletionSubTitle?.ToUpper();
            }
            var positions = _electionService.GetAllPositions();
            _session.Positions = new Stack<Position>();
            foreach (var position in positions)
                _session.Positions.Push(position);
        }
        catch (Exception ex)
        {
            ErrorOccurred?.Invoke("Election Positions Error", ex.Message);
        }
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (!string.IsNullOrWhiteSpace(Pin))
        {
            try
            {
                _currentVoter = await _electionService.LoginVoter(new Voter { VoterCode = Pin });
                ConfirmVoter();
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke("Election Login Error", ex.Message);
            }
        }
    }

    private void ConfirmVoter()
    {
        if (_currentVoter != null)
        {
            if (!_currentVoter.VoteInProgress && !_currentVoter.Voted)
            {
                _session.CurrentVoter = _currentVoter;
                _session.Votes = new ConcurrentBag<Vote>();
                _session.SkippedVotes = new ConcurrentBag<SkippedVotes>();
                LoginSucceeded?.Invoke();
            }
            else
            {
                ErrorOccurred?.Invoke("Error Confirming Voter",
                    "Please Contact Admin Your Details May Not Be Available\n Possible Cause: You May Have Already Voted");
                Pin = string.Empty;
            }
        }
        else
        {
            ErrorOccurred?.Invoke("Error Confirming Voter", "Wrong Code!");
            Pin = string.Empty;
        }
    }
}
