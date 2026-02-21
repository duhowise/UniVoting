using Avalonia.Controls;
using UniVoting.Client.ViewModels;

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
