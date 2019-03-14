using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
        private readonly ConcurrentBag<SkippedVote> _skippedVotes;

        public int CandidateId
        {
            get => (int)GetValue(CandidateIdProperty);
            set => SetValue(CandidateIdProperty, value);
        }


        public delegate void VoteNoEventHandler(object source, EventArgs args);
        public static event VoteNoEventHandler VoteNo;
        public delegate void VoteCastEventHandler(object source, EventArgs args);
        public static readonly DependencyProperty CandidateIdProperty = DependencyProperty.Register("CandidateId", typeof(int), typeof(YesOrNoCandidateControl), new PropertyMetadata(0));
        private readonly CustomDialog _customDialog;
        private  MetroWindow _metroWindow;
        readonly SkipVoteDialogControl _skipDialogControl;
        public static event VoteCastEventHandler VoteCast;


        public YesOrNoCandidateControl(ConcurrentBag<Vote> votes, Position position, Candidate candidate, Voter voter,
            ConcurrentBag<SkippedVote> skippedVotes)
        {
            InitializeComponent();
            _customDialog = new CustomDialog();
            var confirmDialogControl = new ConfirmDialogControl(candidate);
            _skipDialogControl = new SkipVoteDialogControl(position);
            _customDialog.Content = confirmDialogControl;
            _votes = votes;
            _position = position;
            _candidate = candidate;
            _voter = voter;
            _skippedVotes = skippedVotes;
            BtnVoteNo.Click += BtnVoteNo_Click;
            BtnVoteYes.Click += BtnVoteYes_Click;
            _skipDialogControl.BtnNo.Click += SkipBtnNo_Click;
            _skipDialogControl.BtnYes.Click += SkipBtnYes_Click;
            confirmDialogControl.BtnNo.Click += BtnNo_Click;
            confirmDialogControl.BtnYes.Click += BtnYes_Click            ;
            Loaded += YesOrNoCandidateControl_Loaded;
        }

        private async void SkipBtnYes_Click(object sender, RoutedEventArgs e)
        {
            _skippedVotes.Add(new SkippedVote { Positionid = _position.Id, VoterId = _voter.Id });
            OnVoteNo(this);
            await _metroWindow.HideMetroDialogAsync(_customDialog);
        }

        private async void SkipBtnNo_Click(object sender, RoutedEventArgs e)
        {
            await _metroWindow.HideMetroDialogAsync(_customDialog);

        }

        private async void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
            OnVoteCast(this);
            await _metroWindow.HideMetroDialogAsync(_customDialog);
        }

        private async void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            await _metroWindow.HideMetroDialogAsync(_customDialog);

        }

        private void YesOrNoCandidateControl_Loaded(object sender, RoutedEventArgs e)
        {
            _metroWindow = (Window.GetWindow(this) as MetroWindow);
            CandidateId = _candidate.Id;
            CandidateName.Text = _candidate.CandidateName.ToUpper();
            CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
            Rank.Content = $"#{_candidate.RankId}";
        }

        private async void BtnVoteYes_Click(object sender, RoutedEventArgs e)
        {
            BtnVoteYes.IsEnabled = false;
            await _metroWindow.ShowMetroDialogAsync(_customDialog);
            BtnVoteYes.IsEnabled = true;
        }

        private async void BtnVoteNo_Click(object sender, RoutedEventArgs e)
        {
            _customDialog.Content = _skipDialogControl;
         await   _metroWindow.ShowMetroDialogAsync(_customDialog);
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
