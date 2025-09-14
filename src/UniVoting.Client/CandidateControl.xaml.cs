using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;
using Wpf.Ui.Controls;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for CandidateControl.xaml
    /// </summary>
    public partial class CandidateControl : UserControl
    {
        private ConfirmDialogControl confirmDialogControl;
        private FluentWindow _parentWindow;

        public int CandidateId
        {
            get => (int)GetValue(CandidateIdProperty);
            set => SetValue(CandidateIdProperty, value);
        }

        // Using a DependencyProperty as the backing store for CandidateId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CandidateIdProperty =
            DependencyProperty.Register("CandidateId", typeof(int), typeof(CandidateControl), new PropertyMetadata(0));
        public delegate void VoteCastEventHandler(object source, EventArgs args);
        public static event VoteCastEventHandler VoteCast;

        private ConcurrentBag<Vote> _votes;
        private Position _position;
        private Candidate _candidate;
        private Voter _voter;
        public CandidateControl(ConcurrentBag<Vote> votes, Position position, Candidate candidate, Voter voter)
        {
            InitializeComponent();
            this._votes = votes;
            this._position = position;
            this._candidate = candidate;
            this._voter = voter;
            Loaded += CandidateControl_Loaded;
            BtnVote.Click += BtnVote_Click;

            // Initialize dialog control
            confirmDialogControl = new ConfirmDialogControl(candidate);
            confirmDialogControl.BtnYes.Click += BtnYesClick;
            confirmDialogControl.BtnNo.Click += BtnNoClick;
        }

        private void CandidateControl_Loaded(object sender, RoutedEventArgs e)
        {
            CandidateId = _candidate.Id;
            CandidateName.Text = _candidate.CandidateName.ToUpper();
            CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
            Rank.Content = $"#{_candidate.RankId}";
        }

        private async void BtnVote_Click(object sender, RoutedEventArgs e)
        {
            BtnVote.IsEnabled = false;
            _parentWindow = Window.GetWindow(this) as FluentWindow;
            
            // Create and show ContentDialog
            var dialog = new ContentDialog
            {
                Title = "Confirm Vote",
                Content = confirmDialogControl,
                PrimaryButtonText = "Confirm",
                SecondaryButtonText = "Cancel"
            };

            var result = await dialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                // Vote confirmed - handled by BtnYesClick
                _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
                OnVoteCast(this);
            }
            
            BtnVote.IsEnabled = true;
        }

        private static void OnVoteCast(object source)
        {
            VoteCast?.Invoke(source, EventArgs.Empty);
        }

        private void BtnYesClick(object sender, RoutedEventArgs e)
        {
            // This will be handled by the ContentDialog Primary button
        }
        
        private void BtnNoClick(object sender, RoutedEventArgs e)
        {
            // This will be handled by the ContentDialog Secondary button
        }
    }
}
