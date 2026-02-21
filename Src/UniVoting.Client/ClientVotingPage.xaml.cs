using System;
using System.Linq;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Services;
using UniVoting.ViewModels;

namespace UniVoting.Client
{
    public partial class ClientVotingPage : UserControl
    {
        public event Action? VoteCompleted;

        private readonly ClientVotingPageViewModel _viewModel;
        private SkipVoteDialogControl _skipVoteDialogControl = null!;
        private Window? _dialogWindow;
        private readonly IServiceProvider _sp;
        private readonly IClientSessionService _session;

        public ClientVotingPage()
        {
            _session = App.Services.GetRequiredService<IClientSessionService>();
            _sp = App.Services;
            _viewModel = new ClientVotingPageViewModel(_session);
            SetupViewModel();
            InitializeComponent();
            WireHandlers();
        }

        public ClientVotingPage(IClientSessionService session, IServiceProvider sp)
        {
            _session = session;
            _sp = sp;
            _viewModel = new ClientVotingPageViewModel(session);
            SetupViewModel();
            InitializeComponent();
            WireHandlers();
        }

        private void SetupViewModel()
        {
            _skipVoteDialogControl = _sp.GetRequiredService<SkipVoteDialogControl>();
            _viewModel.VoteCompleted += () => VoteCompleted?.Invoke();
            _viewModel.ShowSkipDialog += ShowSkipDialog;
            DataContext = _viewModel;
        }

        private void WireHandlers()
        {
            BtnSkipVote.Click += (_, _) => _viewModel.SkipVoteCommand.Execute(null);
            _skipVoteDialogControl.BtnYes.Click += (_, _) =>
            {
                _viewModel.RecordSkip();
                _dialogWindow?.Close();
            };
            _skipVoteDialogControl.BtnNo.Click += (_, _) => _dialogWindow?.Close();
            Loaded += (_, _) => LoadCandidates();
        }

        private void LoadCandidates()
        {
            _viewModel.Initialize();
            var position = _viewModel.CurrentPosition;
            var voter = _viewModel.CurrentVoter;

            bool isFacultyMatch = string.IsNullOrWhiteSpace(position.Faculty) ||
                position.Faculty.Trim().Equals(voter.Faculty?.Trim(), StringComparison.OrdinalIgnoreCase);

            if (isFacultyMatch)
            {
                if (position.Candidates.Count() == 1)
                {
                    BtnSkipVote.IsEnabled = false;
                    _session.CurrentCandidate = position.Candidates.First();
                    candidatesHolder.Children.Add(_sp.GetRequiredService<YesOrNoCandidateControl>());
                }
                else
                {
                    BtnSkipVote.IsEnabled = true;
                    foreach (var candidate in position.Candidates)
                    {
                        _session.CurrentCandidate = candidate;
                        candidatesHolder.Children.Add(_sp.GetRequiredService<CandidateControl>());
                    }
                }
            }
            else
            {
                VoteCompleted?.Invoke();
            }
        }

        private void ShowSkipDialog()
        {
            var owner = TopLevel.GetTopLevel(this) as Window;
            _dialogWindow = new Window
            {
                Content = _skipVoteDialogControl,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = "Skip Vote"
            };
            _dialogWindow.Show(owner!);
        }
    }
}
