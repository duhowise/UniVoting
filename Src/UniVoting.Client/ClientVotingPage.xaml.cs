using System;
using System.Collections.Concurrent;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Model;
using Position = UniVoting.Model.Position;

namespace UniVoting.Client
{
    public partial class ClientVotingPage : UserControl
    {
        private ConcurrentBag<Vote> _votes;
        private ConcurrentBag<SkippedVotes> _skippedVotes;
        private Position _position;
        private Voter _voter;
        public delegate void VoteCompletedEventHandler(object? source, EventArgs args);
        public event VoteCompletedEventHandler? VoteCompleted;

        private SkipVoteDialogControl _skipVoteDialogControl;
        private Window? _dialogWindow;
        private readonly IServiceProvider _sp;
        private readonly IClientSessionService _session;

        public ClientVotingPage()
        {
            InitializeComponent();
            _session = App.Services.GetRequiredService<IClientSessionService>();
            _votes = _session.Votes;
            _skippedVotes = _session.SkippedVotes;
            _position = _session.CurrentPosition ?? new Position();
            _voter = _session.CurrentVoter ?? new Voter();
            _sp = App.Services;
            _skipVoteDialogControl = _sp.GetRequiredService<SkipVoteDialogControl>();
            _skipVoteDialogControl.BtnYes.Click += BtnYesClick;
            _skipVoteDialogControl.BtnNo.Click += BtnNoClick;
            BtnSkipVote.Click += BtnSkipVote_Click;
            Loaded += ClientVotingPage_Loaded;
        }

        public ClientVotingPage(IClientSessionService session, IServiceProvider sp)
        {
            InitializeComponent();
            _session = session;
            _voter = session.CurrentVoter!;
            _position = session.CurrentPosition!;
            _votes = session.Votes;
            _skippedVotes = session.SkippedVotes;
            _sp = sp;
            BtnSkipVote.Click += BtnSkipVote_Click;
            Loaded += ClientVotingPage_Loaded;

            _skipVoteDialogControl = _sp.GetRequiredService<SkipVoteDialogControl>();
            _skipVoteDialogControl.BtnYes.Click += BtnYesClick;
            _skipVoteDialogControl.BtnNo.Click += BtnNoClick;
        }

        private void ClientVotingPage_Loaded(object? sender, RoutedEventArgs e)
        {
            TextBoxWelcome.Content = $"Welcome, {_voter.VoterName ?? string.Empty}";
            if (string.IsNullOrWhiteSpace(_position.Faculty) ||
                _position.Faculty.Trim().Equals(_voter.Faculty?.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                PositionName.Content = _position.PositionName?.ToUpper();
                if (_position.Candidates.Count() == 1)
                {
                    BtnSkipVote.IsEnabled = false;
                    _session.CurrentCandidate = _position.Candidates.FirstOrDefault()!;
                    candidatesHolder.Children.Add(_sp.GetRequiredService<YesOrNoCandidateControl>());
                }
                else
                {
                    BtnSkipVote.IsEnabled = true;
                    foreach (var candidate in _position.Candidates)
                    {
                        _session.CurrentCandidate = candidate;
                        candidatesHolder.Children.Add(_sp.GetRequiredService<CandidateControl>());
                    }
                }
            }
            else
            {
                OnVoteCompleted(this);
            }
        }

        private void BtnSkipVote_Click(object? sender, RoutedEventArgs e)
        {
            var owner = TopLevel.GetTopLevel(this) as Window;
            _dialogWindow = new Window
            {
                Content = _skipVoteDialogControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "Skip Vote"
            };
            _dialogWindow.Show(owner);
        }

        private void BtnYesClick(object? sender, RoutedEventArgs e)
        {
            _skippedVotes.Add(new SkippedVotes { Positionid = _position.Id, VoterId = _voter.Id });
            OnVoteCompleted(this);
            _dialogWindow?.Close();
        }

        private void BtnNoClick(object? sender, RoutedEventArgs e) => _dialogWindow?.Close();

        private void OnVoteCompleted(object? source) => VoteCompleted?.Invoke(source, EventArgs.Empty);
    }
}
