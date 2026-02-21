using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.LiveView.ViewModels;

public partial class TileViewModel : ObservableObject
{
    private readonly ILiveViewService _liveViewService;
    private readonly ILogger _logger;
    private string _position = string.Empty;
    private readonly bool _isSkipped;

    [ObservableProperty]
    private string _positionText = string.Empty;

    [ObservableProperty]
    private string _voteCount = "0";

    public TileViewModel(ILiveViewService liveViewService, ILogger logger, bool isSkipped = false)
    {
        _liveViewService = liveViewService;
        _logger = logger;
        _isSkipped = isSkipped;
    }

    public void Initialize(string position)
    {
        _position = position.Trim();
        PositionText = _position.ToUpper();
    }

    public async Task UpdateCountAsync()
    {
        try
        {
            int count = _isSkipped
                ? await _liveViewService.VotesSkippedCountAsync(_position)
                : await _liveViewService.VoteCountAsync(_position);
            VoteCount = $"{count}";
        }
        catch (Exception ex)
        {
            _logger.Log(ex);
        }
    }
}
