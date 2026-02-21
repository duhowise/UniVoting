using Avalonia.Controls;
using Avalonia.Interactivity;
using UniVoting.Model;

namespace UniVoting.Client
{
    public partial class ConfirmDialogControl : UserControl
    {
        private readonly Candidate _candidate;

        public ConfirmDialogControl(Candidate candidate)
        {
            _candidate = candidate;
            InitializeComponent();
            Loaded += ConfirmDialogControl_Loaded;
        }

        private void ConfirmDialogControl_Loaded(object? sender, RoutedEventArgs e)
        {
            TextBoxConfirm.Text = $"Are you sure you want to vote for {_candidate.CandidateName}";
            if (_candidate.CandidatePicture != null)
                CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
        }
    }
}
