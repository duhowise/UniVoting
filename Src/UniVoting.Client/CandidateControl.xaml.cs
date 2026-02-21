using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    public partial class CandidateControl : UserControl
    {
        private ConfirmDialogControl _confirmDialogControl;
        private Window _dialogWindow;

        public int CandidateId
        {
            get => (int)GetValue(CandidateIdProperty);
            set => SetValue(CandidateIdProperty, value);
        }

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

            _confirmDialogControl = new ConfirmDialogControl(candidate);
            _confirmDialogControl.BtnYes.Click += BtnYesClick;
            _confirmDialogControl.BtnNo.Click += BtnNoClick;
        }

        private void CandidateControl_Loaded(object sender, RoutedEventArgs e)
        {
            CandidateId = _candidate.Id;
            CandidateName.Text = _candidate.CandidateName.ToUpper();
            CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
            Rank.Content = $"#{_candidate.RankId}";
        }

        private void BtnVote_Click(object sender, RoutedEventArgs e)
        {
            BtnVote.IsEnabled = false;
            _dialogWindow = new Window
            {
                Content = _confirmDialogControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStyle = WindowStyle.ToolWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Window.GetWindow(this),
                Title = "Confirm Vote"
            };
            BtnVote.IsEnabled = true;
            _dialogWindow.Show();
        }

        private static void OnVoteCast(object source)
        {
            VoteCast?.Invoke(source, EventArgs.Empty);
        }

        private void BtnYesClick(object sender, RoutedEventArgs e)
        {
            _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
            OnVoteCast(this);
            _dialogWindow?.Close();
        }

        private void BtnNoClick(object sender, RoutedEventArgs e)
        {
            _dialogWindow?.Close();
        }
    }
}
