using Avalonia.Controls;
using UniVoting.Services;
using UniVoting.ViewModels;

namespace UniVoting.Client
{
    public partial class SkipVoteDialogControl : UserControl
    {
        public SkipVoteDialogControl()
        {
            InitializeComponent();
        }

        public SkipVoteDialogControl(IClientSessionService session)
        {
            DataContext = new SkipVoteDialogViewModel(session);
            InitializeComponent();
        }
    }
}
