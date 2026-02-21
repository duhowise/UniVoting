using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Admin.ViewModels;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminMenuPage : UserControl
    {
        private readonly AdminMenuPageViewModel _viewModel;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminMenuPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminMenuPage(IAdminSessionService session, IElectionConfigurationService electionService, IVotingService votingService, IServiceProvider sp)
        {
            _sp = sp;
            _viewModel = new AdminMenuPageViewModel(session);
            _viewModel.NavigateToCreateAccount += () => MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminCreateAccountPage>());
            _viewModel.NavigateToSetUpElection += () => MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminSetUpElectionPage>());
            _viewModel.NavigateToSetUpPositions += () => MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminSetUpPositionPage>());
            _viewModel.NavigateToSetUpCandidates += () => MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminSetUpCandidatesPage>());
            _viewModel.NavigateToSetUpVoters += () => MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminAddVotersPage>());
            _viewModel.ShowDeclareVotes += () => _sp.GetRequiredService<PresidentLoginWindow>().Show();
            _viewModel.ShowDispensePassword += () => _sp.GetRequiredService<AdminDispensePasswordWindow>().Show();
            DataContext = _viewModel;
            InitializeComponent();
            Loaded += (_, _) => _viewModel.Initialize();
        }
    }
}
