using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class AdminDispensePasswordViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;
    private List<Voter> _allVoters = new();

    public event Action<string, string>? ShowVoterInfo;

    [ObservableProperty]
    private string? _searchTerm;

    [ObservableProperty]
    private ObservableCollection<Voter> _voters = new();

    public AdminDispensePasswordViewModel(IElectionConfigurationService electionService)
    {
        _electionService = electionService;
    }

    partial void OnSearchTermChanged(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var filtered = _allVoters.FindAll(x =>
                (x.VoterName?.ToLower().StartsWith(value.ToLower()) ?? false) ||
                (x.IndexNumber?.ToLower().StartsWith(value.ToLower()) ?? false));
            Voters = new ObservableCollection<Voter>(filtered);
        }
        else
        {
            Voters = new ObservableCollection<Voter>(_allVoters);
        }
    }

    [RelayCommand]
    private async Task RefreshAsync()
    {
        _allVoters = (await _electionService.GetAllVotersAsync()).ToList();
        Voters = new ObservableCollection<Voter>(_allVoters);
        SearchTerm = string.Empty;
    }

    public async Task LoadAsync()
    {
        await RefreshAsync();
    }

    public void ShowSelectedVoterInfo(Voter? voter)
    {
        if (voter != null)
            ShowVoterInfo?.Invoke($"Name: {voter.VoterName}", $"Password: {voter.VoterCode}");
    }
}
