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
    public partial class CandidateControl : UserControl
    {
        private ConfirmDialogControl _confirmDialogControl;
        private Window? _dialogWindow;

        public static readonly StyledProperty<int> CandidateIdProperty =
            AvaloniaProperty.Register<CandidateControl, int>(nameof(CandidateId));

        public int CandidateId
        {
            get => GetValue(CandidateIdProperty);
            set => SetValue(CandidateIdProperty, value);
        }

        public delegate void VoteCastEventHandler(object? source, EventArgs args);
        public static event VoteCastEventHandler? VoteCast;

        private ConcurrentBag<Vote> _votes;
        private Position _position;
        private Candidate _candidate;
        private Voter _voter;
        private readonly IServiceProvider _sp;
        private readonly IClientSessionService _session;

        public CandidateControl()
        {
            InitializeComponent();
            _session = App.Services.GetRequiredService<IClientSessionService>();
            _votes = _session.Votes;
            _position = _session.CurrentPosition ?? new Position();
            _candidate = _session.CurrentCandidate ?? new Candidate();
            _voter = _session.CurrentVoter ?? new Voter();
            _sp = App.Services;
            _confirmDialogControl = _sp.GetRequiredService<ConfirmDialogControl>();
            _confirmDialogControl.BtnYes.Click += BtnYesClick;
            _confirmDialogControl.BtnNo.Click += BtnNoClick;
            Loaded += CandidateControl_Loaded;
            BtnVote.Click += BtnVote_Click;
        }

        public CandidateControl(IClientSessionService session, IServiceProvider sp)
        {
            InitializeComponent();
            _session = session;
            _votes = session.Votes;
            _position = session.CurrentPosition!;
            _candidate = session.CurrentCandidate!;
            _voter = session.CurrentVoter!;
            _sp = sp;
            Loaded += CandidateControl_Loaded;
            BtnVote.Click += BtnVote_Click;

            _confirmDialogControl = _sp.GetRequiredService<ConfirmDialogControl>();
            _confirmDialogControl.BtnYes.Click += BtnYesClick;
            _confirmDialogControl.BtnNo.Click += BtnNoClick;
        }

        private void CandidateControl_Loaded(object? sender, RoutedEventArgs e)
        {
            CandidateId = _candidate.Id;
            CandidateName.Text = _candidate.CandidateName?.ToUpper() ?? string.Empty;
            if (_candidate.CandidatePicture != null)
                CandidateImage.Source = Util.ByteToImageSource(_candidate.CandidatePicture);
            Rank.Content = $"#{_candidate.RankId}";
        }

        private void BtnVote_Click(object? sender, RoutedEventArgs e)
        {
            BtnVote.IsEnabled = false;
            var owner = TopLevel.GetTopLevel(this) as Window;
            _dialogWindow = new Window
            {
                Content = _confirmDialogControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "Confirm Vote"
            };
            BtnVote.IsEnabled = true;
            _dialogWindow.Show(owner);
        }

        private static void OnVoteCast(object? source) => VoteCast?.Invoke(source, EventArgs.Empty);

        private void BtnYesClick(object? sender, RoutedEventArgs e)
        {
            _votes.Add(new Vote { CandidateId = CandidateId, PositionId = _position.Id, VoterId = _voter.Id });
            OnVoteCast(this);
            _dialogWindow?.Close();
        }

        private void BtnNoClick(object? sender, RoutedEventArgs e) => _dialogWindow?.Close();
    }
}
