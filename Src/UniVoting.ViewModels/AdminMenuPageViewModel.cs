using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class AdminMenuPageViewModel : ObservableObject
{
    private readonly IAdminSessionService _session;

    public event Action? NavigateToCreateAccount;
    public event Action? NavigateToSetUpElection;
    public event Action? NavigateToSetUpPositions;
    public event Action? NavigateToSetUpCandidates;
    public event Action? NavigateToSetUpVoters;
    public event Action? ShowDeclareVotes;
    public event Action? ShowDispensePassword;

    [ObservableProperty]
    private bool _isCreateAccountEnabled = true;

    [ObservableProperty]
    private bool _isSetUpElectionEnabled = true;

    [ObservableProperty]
    private bool _isSetUpPositionsEnabled = true;

    [ObservableProperty]
    private bool _isSetUpCandidatesEnabled = true;

    [ObservableProperty]
    private bool _isSetUpVotersEnabled = true;

    [ObservableProperty]
    private bool _isDeclareVotesEnabled = true;

    [ObservableProperty]
    private bool _isDispensePasswordEnabled = true;

    public AdminMenuPageViewModel(IAdminSessionService session)
    {
        _session = session;
    }

    public void Initialize()
    {
        var comissioner = _session.CurrentAdmin;
        if (comissioner == null) return;
        if (comissioner.IsChairman)
        {
            IsSetUpElectionEnabled = false;
            IsCreateAccountEnabled = false;
            IsSetUpCandidatesEnabled = false;
            IsSetUpPositionsEnabled = false;
        }
        else if (comissioner.IsPresident)
        {
            IsSetUpElectionEnabled = false;
            IsCreateAccountEnabled = false;
            IsSetUpCandidatesEnabled = false;
            IsSetUpPositionsEnabled = false;
            IsSetUpVotersEnabled = false;
        }
        else if (!comissioner.IsChairman && !comissioner.IsAdmin && !comissioner.IsPresident)
        {
            IsSetUpElectionEnabled = false;
            IsCreateAccountEnabled = false;
            IsSetUpCandidatesEnabled = false;
            IsSetUpPositionsEnabled = false;
            IsDeclareVotesEnabled = false;
        }
    }

    [RelayCommand]
    private void CreateAccount() => NavigateToCreateAccount?.Invoke();

    [RelayCommand]
    private void SetUpElection() => NavigateToSetUpElection?.Invoke();

    [RelayCommand]
    private void SetUpPositions() => NavigateToSetUpPositions?.Invoke();

    [RelayCommand]
    private void SetUpCandidates() => NavigateToSetUpCandidates?.Invoke();

    [RelayCommand]
    private void SetUpVoters() => NavigateToSetUpVoters?.Invoke();

    [RelayCommand]
    private void DeclareVotes() => ShowDeclareVotes?.Invoke();

    [RelayCommand]
    private void DispensePassword() => ShowDispensePassword?.Invoke();
}
