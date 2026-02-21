using Avalonia.Controls;
using UniVoting.Client.ViewModels;

namespace UniVoting.Client
{
    public partial class ConfirmDialogControl : UserControl
    {
        public ConfirmDialogControl()
        {
            InitializeComponent();
        }

        public ConfirmDialogControl(IClientSessionService session)
        {
            DataContext = new ConfirmDialogViewModel(session);
            InitializeComponent();
        }
    }
}
