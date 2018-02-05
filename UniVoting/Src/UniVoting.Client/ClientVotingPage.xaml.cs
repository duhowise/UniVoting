using System;
using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
using Position = UniVoting.Model.Position;
using System.Linq;

namespace UniVoting.Client
{
    /// <summary>
    /// Interaction logic for ClientVotingPage.xaml
    /// </summary>
    public partial class ClientVotingPage : Page
    {
        private ConcurrentBag<Vote> _votes;
        private ConcurrentBag<SkippedVotes> _skippedVotes;
        private Position _position;
        private Voter _voter;
        public delegate void VoteCompletedEventHandler(object source, EventArgs args);
        public event VoteCompletedEventHandler VoteCompleted;

        private CustomDialog _customDialog;
        private SkipVoteDialogControl skipVoteDialogControl;
        private MetroWindow _metroWindow;

        public ClientVotingPage(Voter voter, Position position, ConcurrentBag<Vote> votes, ConcurrentBag<SkippedVotes> skippedVotes)
        {
            InitializeComponent();
            this._voter = voter;
            this._position = position;
            _votes = votes;
            _skippedVotes = skippedVotes;
            BtnSkipVote.Click += BtnSkipVote_Click;
            Loaded += ClientVotingPage_Loaded;

            _customDialog = new CustomDialog();
            skipVoteDialogControl = new SkipVoteDialogControl();
            skipVoteDialogControl.BtnYes.Click += BtnYesClick;
            skipVoteDialogControl.BtnNo.Click += BtnNoClick;
            _customDialog.Content = skipVoteDialogControl;

        }


        private void ClientVotingPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            TextBoxWelcome.Content = $"Welcome, {_voter.VoterName ?? String.Empty}";

            if (string.IsNullOrWhiteSpace(_position.Faculty) || _position.Faculty.Trim().Equals(_voter.Faculty.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                PositionName.Content = _position.PositionName.ToUpper();
                if (_position.Candidates.Count() == 1)
                {
                    BtnSkipVote.IsEnabled = false;
                    candidatesHolder.Children.Add(new YesOrNoCandidateControl(_votes, _position, _position.Candidates.FirstOrDefault(), _voter, _skippedVotes));
                }
                else
                {
                    BtnSkipVote.IsEnabled = true;
                    foreach (var candidate in _position.Candidates)
                    {
                        candidatesHolder.Children.Add(new CandidateControl(_votes, _position, candidate, _voter));
                    }
                }
            }
            else
            {
                OnVoteCompleted(this);
            }
        }

        private async void BtnSkipVote_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            var metroWindow = (Window.GetWindow(this) as MetroWindow);
            var dialogSettings = new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Accented,
                AffirmativeButtonText = "OK",
                AnimateShow = true,
                NegativeButtonText = "Cancel",
                FirstAuxiliaryButtonText = "Cancel",
                DialogMessageFontSize = 18
            };
            //var dialogSettings = new MetroDialogSettings { DialogMessageFontSize = 18, AffirmativeButtonText = "Ok" };
            
            await _metroWindow.ShowMetroDialogAsync(_customDialog,dialogSettings);

            //MessageDialogResult result = await metroWindow.ShowMessageAsync("Skip Vote", $"Are You Sure You Want to Skip {_position.PositionName} ?", MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
        }

        private void BtnYesClick(object sender, RoutedEventArgs e)
        {
            _skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
            OnVoteCompleted(this);

        }
        private async void BtnNoClick(object sender, RoutedEventArgs e)
        {
            await _metroWindow.HideMetroDialogAsync(_customDialog);
        }

        private void OnVoteCompleted(object source)
        {
            VoteCompleted?.Invoke(source, EventArgs.Empty);
        }
    }
}
