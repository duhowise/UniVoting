using Avalonia.Controls;
using Avalonia.Interactivity;
using UniVoting.Model;

namespace UniVoting.Client
{
    public partial class ConfirmDialogControl : UserControl
    {
        private readonly Candidate _candidate;

        public ConfirmDialogControl()
        {
            InitializeComponent();
            _candidate = new Candidate();
        }

        public ConfirmDialogControl(IClientSessionService session)
        {
            _candidate = session.CurrentCandidate!;
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
