using Avalonia.Controls;
using Avalonia.Interactivity;
using UniVoting.Model;

namespace UniVoting.Client
{
    public partial class SkipVoteDialogControl : UserControl
    {
        private readonly Position _position;

        public SkipVoteDialogControl() { InitializeComponent(); _position = new Position(); }

        public SkipVoteDialogControl(Position position)
        {
            _position = position;
            InitializeComponent();
            Loaded += SkipVoteDialogControl_Loaded;
        }

        private void SkipVoteDialogControl_Loaded(object? sender, RoutedEventArgs e)
        {
            TextBoxConfirm.Text = $"Are You Sure You Want to Skip {_position.PositionName} ?";
        }
    }
}
