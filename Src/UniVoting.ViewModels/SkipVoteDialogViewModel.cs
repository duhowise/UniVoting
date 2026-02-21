using CommunityToolkit.Mvvm.ComponentModel;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.ViewModels;

public partial class SkipVoteDialogViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _confirmText;

    public SkipVoteDialogViewModel(IClientSessionService session)
    {
        var position = session.CurrentPosition ?? new Position();
        ConfirmText = $"Are You Sure You Want to Skip {position.PositionName} ?";
    }
}
