using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
