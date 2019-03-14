using System.Windows.Controls;
using Univoting.Core;

namespace UniVoting.Client
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for SkipVoteDialogControl.xaml
    /// </summary>
    public partial class SkipVoteDialogControl : UserControl
    {
        private readonly Position _position;

        public SkipVoteDialogControl(Position position)
        {
            _position = position;
            InitializeComponent();
            Loaded += SkipVoteDialogControl_Loaded;
        }

        private void SkipVoteDialogControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBoxConfirm.Text = $"Are You Sure You Want to Skip {_position.PositionName} ?";
        }
    }
}
