using CommunityToolkit.Mvvm.ComponentModel;
using UniVoting.Model;

namespace UniVoting.Client.ViewModels;

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
