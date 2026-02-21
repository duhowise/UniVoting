using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class LiveViewMainWindowViewModel : ObservableObject
{
    private readonly ILiveViewService _liveViewService;
    private readonly ILogger _logger;

    public event Action<IEnumerable<Position>>? PositionsLoaded;

    public LiveViewMainWindowViewModel(ILiveViewService liveViewService, ILogger logger)
    {
        _liveViewService = liveViewService;
        _logger = logger;
    }

    public async Task LoadAsync()
    {
        try
        {
            var positions = await _liveViewService.Positions();
            PositionsLoaded?.Invoke(positions);
        }
        catch (Exception ex)
        {
            _logger.Log(ex);
        }
    }
}
