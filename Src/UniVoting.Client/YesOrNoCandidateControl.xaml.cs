using System;
using System.Collections.Concurrent;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Model;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    public partial class YesOrNoCandidateControl : UserControl
    {
        private readonly ConcurrentBag<Vote> _votes;
        private readonly Position _position;
        private readonly Candidate _candidate;
        private readonly Voter _voter;
        private readonly ConcurrentBag<SkippedVotes> _skippedVotes;
        private Window? _dialogWindow;

        public static readonly StyledProperty<int> CandidateIdProperty =
            AvaloniaProperty.Register<YesOrNoCandidateControl, int>(nameof(CandidateId));

        public int CandidateId
        {
            get => GetValue(CandidateIdProperty);
            set => SetValue(CandidateIdProperty, value);
        }

        public delegate void VoteNoEventHandler(object? source, EventArgs args);
        public static event VoteNoEventHandler? VoteNo;
        public delegate void VoteCastEventHandler(object? source, EventArgs args);
        public static event VoteCastEventHandler? VoteCast;

        private readonly ConfirmDialogControl _confirmDialogControl;
        private readonly SkipVoteDialogControl _skipDialogControl;

        public YesOrNoCandidateControl()
        {
            InitializeComponent();
            _votes = new ConcurrentBag<Vote>();
            _position = new Position();
            _candidate = new Candidate();
            _voter = new Voter();
            _skippedVotes = new ConcurrentBag<SkippedVotes>();
            _confirmDialogControl = new ConfirmDialogControl();
            _skipDialogControl = new SkipVoteDialogControl();
        }

        public YesOrNoCandidateControl(ConcurrentBag<Vote> votes, Position position, Candidate candidate, Voter voter,
            ConcurrentBag<SkippedVotes> skippedVotes, IServiceProvider sp)
        {
            InitializeComponent();
            _confirmDialogControl = ActivatorUtilities.CreateInstance<ConfirmDialogControl>(sp, candidate);
            _skipDialogControl = ActivatorUtilities.CreateInstance<SkipVoteDialogControl>(sp, position);
            _votes = votes;
            _position = position;
            _candidate = candidate;
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

        private void SkipBtnYes_Click(object? sender, RoutedEventArgs e)
        {
            _skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
            OnVoteNo(this);
            _dialogWindow?.Close();
        }

        private void SkipBtnNo_Click(object? sender, RoutedEventArgs e) => _dialogWindow?.Close();

        private void BtnYes_Click(object? sender, RoutedEventArgs e)
        {
            _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
            OnVoteCast(this);
            _dialogWindow?.Close();
        }

        private void BtnNo_Click(object? sender, RoutedEventArgs e) => _dialogWindow?.Close();

        private void YesOrNoCandidateControl_Loaded(object? sender, RoutedEventArgs e)
        {
            CandidateId = _candidate.Id;
            CandidateName.Text = _candidate.CandidateName?.ToUpper() ?? string.Empty;
            if (_candidate.CandidatePicture != null)
                CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
            Rank.Content = $"#{_candidate.RankId}";
        }

        private void BtnVoteYes_Click(object? sender, RoutedEventArgs e)
        {
            BtnVoteYes.IsEnabled = false;
            var owner = TopLevel.GetTopLevel(this) as Window;
            _dialogWindow = new Window
            {
                Content = _confirmDialogControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "Confirm Vote"
            };
            BtnVoteYes.IsEnabled = true;
            _dialogWindow.Show(owner);
        }

        private void BtnVoteNo_Click(object? sender, RoutedEventArgs e)
        {
            var owner = TopLevel.GetTopLevel(this) as Window;
            _dialogWindow = new Window
            {
                Content = _skipDialogControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "Skip Vote"
            };
            _dialogWindow.Show(owner);
        }

        private static void OnVoteCast(object? source) => VoteCast?.Invoke(source, EventArgs.Empty);
        private static void OnVoteNo(object? source) => VoteNo?.Invoke(source, EventArgs.Empty);
    }
}
