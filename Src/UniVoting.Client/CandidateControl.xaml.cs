using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Client.ViewModels;

namespace UniVoting.Client
{
    public partial class CandidateControl : UserControl
    {
        private CandidateControlViewModel _viewModel = null!;
        private Window? _dialogWindow;

        public static event System.Action? VoteCast
        {
            add => CandidateControlViewModel.VoteCast += value;
            remove => CandidateControlViewModel.VoteCast -= value;
        }

        public CandidateControl()
        {
            InitializeComponent();
            var session = App.Services.GetRequiredService<IClientSessionService>();
            SetupViewModel(session, App.Services);
        }

        public CandidateControl(IClientSessionService session, IServiceProvider sp)
        {
            InitializeComponent();
            SetupViewModel(session, sp);
        }

        private void SetupViewModel(IClientSessionService session, IServiceProvider sp)
        {
            _viewModel = new CandidateControlViewModel(session);
            DataContext = _viewModel;

            var confirmDialog = sp.GetRequiredService<ConfirmDialogControl>();
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
            _viewModel.CloseDialog += () => _dialogWindow?.Close();
            BtnVote.Click += (_, _) => _viewModel.VoteCommand.Execute(null);
            confirmDialog.BtnYes.Click += (_, _) => _viewModel.ConfirmVote();
            confirmDialog.BtnNo.Click += (_, _) => _viewModel.CancelVote();
        }
    }
}
