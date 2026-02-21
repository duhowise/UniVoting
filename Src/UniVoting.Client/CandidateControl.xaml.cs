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

        public CandidateControl()
        {
            InitializeComponent();
            _votes = new ConcurrentBag<Vote>();
            _position = new Position();
            _candidate = new Candidate();
            _voter = new Voter();
            _sp = App.Services;
            _confirmDialogControl = _sp.GetRequiredService<ConfirmDialogControl>();
        }

        public CandidateControl(ConcurrentBag<Vote> votes, Position position, Candidate candidate, Voter voter, IServiceProvider sp)
        {
            InitializeComponent();
            _votes = votes;
            _position = position;
            _candidate = candidate;
            _voter = voter;
            _sp = sp;
            Loaded += CandidateControl_Loaded;
            BtnVote.Click += BtnVote_Click;

            _confirmDialogControl = ActivatorUtilities.CreateInstance<ConfirmDialogControl>(_sp, candidate);
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
