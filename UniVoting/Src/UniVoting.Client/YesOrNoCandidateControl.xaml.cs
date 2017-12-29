using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
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
        private ConcurrentBag<Vote> _votes;
        private Model.Position _position;
        private Candidate _candidate;
        private Voter _voter;
        private  ConcurrentBag<SkippedVotes> _skippedVotes;

        public int CandidateId
        {
            get => (int)GetValue(CandidateIdProperty);
            set => SetValue(CandidateIdProperty, value);
        }


        public  delegate void VoteNoEventHandler(object source, EventArgs args);

        public static event VoteNoEventHandler VoteNo;
        public delegate void VoteCastEventHandler(object source, EventArgs args);
        public static readonly DependencyProperty CandidateIdProperty =DependencyProperty.Register("CandidateId", typeof(int), typeof(YesOrNoCandidateControl), new PropertyMetadata(0));
        public static event VoteCastEventHandler VoteCast;



        public YesOrNoCandidateControl(ConcurrentBag<Vote> votes, Position position, Candidate candidate, Voter voter,
            ConcurrentBag<SkippedVotes> skippedVotes)
        {
            InitializeComponent();
            _votes = votes;
            _position = position;
            this._candidate = candidate;
            _voter = voter;
            _skippedVotes = skippedVotes;
            BtnVoteNo.Click += BtnVoteNo_Click;
            BtnVoteYes.Click += BtnVoteYes_Click;
            Loaded += YesOrNoCandidateControl_Loaded            ;
        }

        private void YesOrNoCandidateControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            CandidateId = _candidate.Id;
            CandidateName.Text = _candidate.CandidateName.ToUpper();
            CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
            Rank.Content = $"#{_candidate.RankId}";
        }

        private async void BtnVoteYes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BtnVoteYes.IsEnabled = false;
            var metroWindow = (Window.GetWindow(this) as MetroWindow);
            var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 18, AffirmativeButtonText = "Ok", };
            MessageDialogResult result = await metroWindow.ShowMessageAsync("Vote Yes", $"Are You Sure You Want to Vote Yes For {_candidate.CandidateName} ?", MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
            if (result == MessageDialogResult.Affirmative)
            {
                _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
                OnVoteCast(this);
            }
            BtnVoteYes.IsEnabled = true;
        }

        private async void BtnVoteNo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var metroWindow = (Window.GetWindow(this) as MetroWindow);
            var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 18, AffirmativeButtonText = "Ok" };



            MessageDialogResult result = await metroWindow.ShowMessageAsync("Skip Vote", $"Are You Sure You Want to Skip {_position.PositionName} ?", MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
            if (result == MessageDialogResult.Affirmative)
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
