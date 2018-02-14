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
        private ConcurrentBag<SkippedVotes> _skippedVotes;

        public int CandidateId
        {
            get => (int)GetValue(CandidateIdProperty);
            set => SetValue(CandidateIdProperty, value);
        }


        public delegate void VoteNoEventHandler(object source, EventArgs args);

        public static event VoteNoEventHandler VoteNo;
        public delegate void VoteCastEventHandler(object source, EventArgs args);
        public static readonly DependencyProperty CandidateIdProperty = DependencyProperty.Register("CandidateId", typeof(int), typeof(YesOrNoCandidateControl), new PropertyMetadata(0));
        private CustomDialog _customDialog;
        private  MetroWindow _metroWindow;
        readonly SkipVoteDialogControl _skipDialogControl;

        public static event VoteCastEventHandler VoteCast;



        public YesOrNoCandidateControl(ConcurrentBag<Vote> votes, Position position, Candidate candidate, Voter voter,
            ConcurrentBag<SkippedVotes> skippedVotes)
        {
            InitializeComponent();
            _customDialog = new CustomDialog();
            var confirmDialogControl = new ConfirmDialogControl(candidate);
            _skipDialogControl = new SkipVoteDialogControl();
            _customDialog.Content = confirmDialogControl;
            _votes = votes;
            _position = position;
            this._candidate = candidate;
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
            _skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
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
            //var dialogSettings = new MetroDialogSettings
            //{
            //    ColorScheme = MetroDialogColorScheme.Accented,
            //    AffirmativeButtonText = "OK",
            //    AnimateShow = true,
            //    NegativeButtonText = "Cancel",
            //    FirstAuxiliaryButtonText = "Cancel",
            //    DialogMessageFontSize = 18
            //};
            //var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 18, AffirmativeButtonText = "Ok", };
            //MessageDialogResult result = await metroWindow.ShowMessageAsync("Vote Yes", $"Are You Sure You Want to Vote Yes For {_candidate.CandidateName} ?", MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
            //if (result == MessageDialogResult.Affirmative)
            //{
               
            //}
            await _metroWindow.ShowMetroDialogAsync(_customDialog);
            BtnVoteYes.IsEnabled = true;
        }

        private async void BtnVoteNo_Click(object sender, RoutedEventArgs e)
        {
            //_metroWindow = (Window.GetWindow(this) as MetroWindow);
            //var dialogSettings = new MetroDialogSettings
            //{
            //    ColorScheme = MetroDialogColorScheme.Accented,
            //    AffirmativeButtonText = "OK",
            //    AnimateShow = true,
            //    NegativeButtonText = "Cancel",
            //    FirstAuxiliaryButtonText = "Cancel",
            //    DialogMessageFontSize = 18
            //};
            //var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 18, AffirmativeButtonText = "Ok" };
            _customDialog.Content = _skipDialogControl;
         await   _metroWindow.ShowMetroDialogAsync(_customDialog);
            //MessageDialogResult result = await _metroWindow.ShowMessageAsync("Skip Vote", $"Are You Sure You Want to Skip {_position.PositionName} ?", MessageDialogStyle.AffirmativeAndNegative);
            //if (result == MessageDialogResult.Affirmative)
            //{
            //    //_skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
            //    //OnVoteNo(this);

            //}
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
