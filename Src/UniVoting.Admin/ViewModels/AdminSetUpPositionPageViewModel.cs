using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.ViewModels;

public partial class AdminSetUpPositionPageViewModel : ObservableObject
{
    private readonly IElectionConfigurationService _electionService;

    public event Action? ShowAddPositionDialog;
    public event Action? CloseDialog;

    [ObservableProperty]
    private ObservableCollection<Position> _positions = new();

    [ObservableProperty]
    private string? _newPositionName;

    [ObservableProperty]
    private string? _newFaculty;

    public AdminSetUpPositionPageViewModel(IElectionConfigurationService electionService)
    {
        _electionService = electionService;
    }

    public async Task LoadAsync()
    {
        var positions = await _electionService.GetAllPositionsAsync();
        Positions = new ObservableCollection<Position>(positions);
    }

    [RelayCommand]
    private void AddPosition() => ShowAddPositionDialog?.Invoke();

    [RelayCommand]
    private async Task SavePositionAsync()
    {
        await _electionService.AddPosition(new Position { PositionName = NewPositionName, Faculty = NewFaculty });
        var position = new Position { PositionName = NewPositionName, Faculty = NewFaculty };
        Positions.Add(position);
        NewPositionName = string.Empty;
        NewFaculty = string.Empty;
        CloseDialog?.Invoke();
    }

    [RelayCommand]
    private void CancelAdd() => CloseDialog?.Invoke();

    public async Task UpdatePositionAsync(int id, string? positionName, string? faculty)
    {
        await _electionService.UpdatePosition(new Position { Id = id, PositionName = positionName, Faculty = faculty });
    }

    public void RemovePosition(Position position)
    {
        _electionService.RemovePosition(position);
        Positions.Remove(position);
    }
}
