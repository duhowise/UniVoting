using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;
using Wpf.Ui.Controls;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    /// Interaction logic for YesOrNoCandidateControl.xaml
    /// </summary>
    public partial class YesOrNoCandidateControl : UserControl
    {
        private readonly ConcurrentBag<Vote> _votes;
        private readonly Position _position;
        private readonly Candidate _candidate;
        private readonly Voter _voter;
        private readonly ConcurrentBag<SkippedVotes> _skippedVotes;

        public int CandidateId
        {
            get => (int)GetValue(CandidateIdProperty);
            set => SetValue(CandidateIdProperty, value);
        }

        public delegate void VoteNoEventHandler(object source, EventArgs args);
        public static event VoteNoEventHandler VoteNo;
        public delegate void VoteCastEventHandler(object source, EventArgs args);
        public static readonly DependencyProperty CandidateIdProperty = DependencyProperty.Register("CandidateId", typeof(int), typeof(YesOrNoCandidateControl), new PropertyMetadata(0));
        
        private FluentWindow _parentWindow;
        readonly SkipVoteDialogControl _skipDialogControl;
        private ConfirmDialogControl _confirmDialogControl;
        public static event VoteCastEventHandler VoteCast;

        public YesOrNoCandidateControl(ConcurrentBag<Vote> votes, Position position, Candidate candidate, Voter voter,
            ConcurrentBag<SkippedVotes> skippedVotes)
        {
            InitializeComponent();
            _confirmDialogControl = new ConfirmDialogControl(candidate);
            _skipDialogControl = new SkipVoteDialogControl(position);
            _votes = votes;
            _position = position;
            this._candidate = candidate;
            _voter = voter;
            _skippedVotes = skippedVotes;
            BtnVoteNo.Click += BtnVoteNo_Click;
            BtnVoteYes.Click += BtnVoteYes_Click;
            _skipDialogControl.BtnNo.Click += SkipBtnNo_Click;
            _skipDialogControl.BtnYes.Click += SkipBtnYes_Click;
            _confirmDialogControl.BtnNo.Click += BtnNo_Click;
            _confirmDialogControl.BtnYes.Click += BtnYes_Click;
            Loaded += YesOrNoCandidateControl_Loaded;
        }

        private void SkipBtnYes_Click(object sender, RoutedEventArgs e)
        {
            _skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
            OnVoteNo(this);
        }

        private void SkipBtnNo_Click(object sender, RoutedEventArgs e)
        {
            // Dialog will close automatically
        }

        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
            OnVoteCast(this);
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            // Dialog will close automatically
        }

        private void YesOrNoCandidateControl_Loaded(object sender, RoutedEventArgs e)
        {
            _parentWindow = Window.GetWindow(this) as FluentWindow;
            CandidateId = _candidate.Id;
            CandidateName.Text = _candidate.CandidateName.ToUpper();
            CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
            Rank.Content = $"#{_candidate.RankId}";
        }

        private async void BtnVoteYes_Click(object sender, RoutedEventArgs e)
        {
            BtnVoteYes.IsEnabled = false;
            
            var dialog = new ContentDialog
            {
                Title = "Confirm Vote",
                Content = _confirmDialogControl,
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };

            var result = await dialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
                OnVoteCast(this);
            }
            
            BtnVoteYes.IsEnabled = true;
        }

        private async void BtnVoteNo_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Skip Vote",
                Content = _skipDialogControl,
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };

            var result = await dialog.ShowAsync();
            
            if (result == ContentDialogResult.Primary)
            {
                _skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
                OnVoteNo(this);
            }
        }
        
        private static void OnVoteCast(object source)
        {
            VoteCast?.Invoke(source, EventArgs.Empty);
        }

        private static void OnVoteNo(object source)
        {
            VoteNo?.Invoke(source, EventArgs.Empty);
        }
    }

}
