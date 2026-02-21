using Avalonia.Controls;
using UniVoting.Services;
using UniVoting.ViewModels;

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
