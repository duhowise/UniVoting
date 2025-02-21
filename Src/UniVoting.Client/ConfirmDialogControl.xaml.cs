using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for ConfirmDialogControl.xaml
    /// </summary>
    public partial class ConfirmDialogControl : UserControl
    {
        private readonly Candidate _candidate;

        public ConfirmDialogControl(Candidate candidate)
        {
            _candidate = candidate;
            InitializeComponent();
            Loaded += ConfirmDialogControl_Loaded;

            
        }

       private void ConfirmDialogControl_Loaded(object sender, RoutedEventArgs e)
       {
           TextBoxConfirm.Text = $"Are you sure you want to vote for {_candidate.CandidateName}";
           CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
       }


    }
}
