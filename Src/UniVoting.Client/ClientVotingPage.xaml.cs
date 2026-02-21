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

        public ClientVotingPage()
        {
            InitializeComponent();
            _votes = new ConcurrentBag<Vote>();
            _skippedVotes = new ConcurrentBag<SkippedVotes>();
            _position = new Position();
            _voter = new Voter();
            _skipVoteDialogControl = new SkipVoteDialogControl();
            _sp = null!;
        }

        public ClientVotingPage(Voter voter, Position position, ConcurrentBag<Vote> votes, ConcurrentBag<SkippedVotes> skippedVotes, IServiceProvider sp)
        {
            InitializeComponent();
            _voter = voter;
            _position = position;
            _votes = votes;
            _skippedVotes = skippedVotes;
            _sp = sp;
            BtnSkipVote.Click += BtnSkipVote_Click;
            Loaded += ClientVotingPage_Loaded;

            _skipVoteDialogControl = ActivatorUtilities.CreateInstance<SkipVoteDialogControl>(_sp, position);
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
                    candidatesHolder.Children.Add(ActivatorUtilities.CreateInstance<YesOrNoCandidateControl>(_sp, _votes, _position, _position.Candidates.FirstOrDefault()!, _voter, _skippedVotes));
                }
                else
                {
                    BtnSkipVote.IsEnabled = true;
                    foreach (var candidate in _position.Candidates)
                        candidatesHolder.Children.Add(ActivatorUtilities.CreateInstance<CandidateControl>(_sp, _votes, _position, candidate, _voter));
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
