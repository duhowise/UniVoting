using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.ViewModels;

public partial class AdminAddVotersPageViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;
    private readonly IVotingService _votingService;
    private List<Voter> _pendingVoters = new();

    public event Func<Task<string?>>? PickExcelFileRequested;
    public event Action<string, string>? ShowMessage;

    [ObservableProperty]
    private string? _voterName;

    [ObservableProperty]
    private string? _indexNumber;

    [ObservableProperty]
    private string? _searchTerm;

    [ObservableProperty]
    private string? _resetIndexNumber;

    [ObservableProperty]
    private string? _importedFilename;

    [ObservableProperty]
    private string? _addedCountText;

    [ObservableProperty]
    private bool _isAddedCountVisible;

    [ObservableProperty]
    private bool _isSaveEnabled = true;

    [ObservableProperty]
    private ObservableCollection<Voter> _voters = new();

    public AdminAddVotersPageViewModel(IElectionConfigurationService electionService, IVotingService votingService)
    {
        _electionService = electionService;
        _votingService = votingService;
    }

    public void AddVoterFromFields()
    {
        if (!string.IsNullOrWhiteSpace(VoterName) && !string.IsNullOrWhiteSpace(IndexNumber))
        {
            _pendingVoters.Add(new Voter
            {
                VoterName = VoterName,
                IndexNumber = IndexNumber,
                VoterCode = Util.GenerateRandomPassword(6)
            });
        }
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            var voter = await _votingService.GetVoterPass(new Voter { IndexNumber = SearchTerm });
            if (voter != null)
                ShowMessage?.Invoke($"Name: {voter.VoterName}", $"Password: {voter.VoterCode}");
            else
                ShowMessage?.Invoke("Password", $"Voter with Index Number: {SearchTerm} not found!");
            SearchTerm = string.Empty;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (_pendingVoters.Count != 0)
        {
            // confirmation is done in code-behind via event
            try
            {
                IsSaveEnabled = false;
                var added = await _electionService.AddVotersAsync(_pendingVoters);
                IsAddedCountVisible = true;
                AddedCountText = $"Added {added} Voters";
                _pendingVoters.Clear();
            }
            catch (Exception ex)
            {
                ShowMessage?.Invoke("Voter Addition Error", ex.Message);
            }
            finally
            {
                IsSaveEnabled = true;
            }
        }
    }

    [RelayCommand]
    private async Task ResetVoterAsync()
    {
        if (!string.IsNullOrWhiteSpace(ResetIndexNumber))
        {
            await _votingService.ResetVoter(new Voter { IndexNumber = ResetIndexNumber });
            ShowMessage?.Invoke("Success", "Successfully reset Voter");
        }
    }

    public void AddVotersFromData(List<Voter> importedVoters, string filename)
    {
        _pendingVoters.AddRange(importedVoters);
        Voters = new ObservableCollection<Voter>(_pendingVoters);
        ImportedFilename = $"File Name {filename}";
    }

    public bool HasPendingVoters => _pendingVoters.Count > 0;
}
