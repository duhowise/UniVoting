using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Services;
using UniVoting.ViewModels;

namespace UniVoting.Client
{
    public partial class YesOrNoCandidateControl : UserControl
    {
        private YesOrNoCandidateViewModel _viewModel = null!;
        private Window? _dialogWindow;

        public static event Action? VoteNo
        {
            add => YesOrNoCandidateViewModel.VoteNo += value;
            remove => YesOrNoCandidateViewModel.VoteNo -= value;
        }

        public static event Action? VoteCast
        {
            add => YesOrNoCandidateViewModel.VoteCast += value;
            remove => YesOrNoCandidateViewModel.VoteCast -= value;
        }

        public YesOrNoCandidateControl()
        {
            InitializeComponent();
            var session = App.Services.GetRequiredService<IClientSessionService>();
            SetupViewModel(session, App.Services);
        }

        public YesOrNoCandidateControl(IClientSessionService session, IServiceProvider sp)
        {
            InitializeComponent();
            SetupViewModel(session, sp);
        }

        private void SetupViewModel(IClientSessionService session, IServiceProvider sp)
        {
            _viewModel = new YesOrNoCandidateViewModel(session);
            DataContext = _viewModel;

            var confirmDialog = sp.GetRequiredService<ConfirmDialogControl>();
            var skipDialog = sp.GetRequiredService<SkipVoteDialogControl>();

            _viewModel.ShowConfirmDialog += () =>
            {
                var owner = Avalonia.Controls.TopLevel.GetTopLevel(this) as Window;
                _dialogWindow = new Window
                {
                    Content = confirmDialog,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Title = "Confirm Vote"
                };
                _dialogWindow.Show(owner!);
            };
            _viewModel.ShowSkipDialog += () =>
            {
                var owner = Avalonia.Controls.TopLevel.GetTopLevel(this) as Window;
                _dialogWindow = new Window
                {
                    Content = skipDialog,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Title = "Skip Vote"
                };
                _dialogWindow.Show(owner!);
            };
            _viewModel.CloseDialog += () => _dialogWindow?.Close();

            BtnVoteYes.Click += (_, _) => _viewModel.VoteYesCommand.Execute(null);
            BtnVoteNo.Click += (_, _) => _viewModel.VoteNoBtnClickCommand.Execute(null);

            confirmDialog.BtnYes.Click += (_, _) => _viewModel.ConfirmYesVote();
            confirmDialog.BtnNo.Click += (_, _) => _viewModel.CancelDialog();
            skipDialog.BtnYes.Click += (_, _) => _viewModel.ConfirmSkip();
            skipDialog.BtnNo.Click += (_, _) => _viewModel.CancelDialog();
        }
    }
}
