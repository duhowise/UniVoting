using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminMenuPage : UserControl
    {
        private IAdminSessionService _session;
        private readonly IElectionConfigurationService _electionService;
        private readonly IVotingService _votingService;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminMenuPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminMenuPage(IAdminSessionService session, IElectionConfigurationService electionService, IVotingService votingService, IServiceProvider sp)
        {
            _session = session;
            _electionService = electionService;
            _votingService = votingService;
            _sp = sp;
            InitializeComponent();
            BtnDeclareVotes.Click += BtnDeclareVotes_Click;
            BtnDispensePassword.Click += BtnDispensePassword_Click;
            Loaded += AdminMenuPage_Loaded;
        }

        private void BtnDispensePassword_Click(object? sender, RoutedEventArgs e)
        {
            _sp.GetRequiredService<AdminDispensePasswordWindow>().Show();
        }

        private void AdminMenuPage_Loaded(object? sender, RoutedEventArgs e)
        {
            var comissioner = _session.CurrentAdmin;
            if (comissioner == null) return;
            if (comissioner.IsChairman)
            {
                BtnSetUpElection.IsEnabled = false;
                BtnCreateAccount.IsEnabled = false;
                BtnSetUpCandidates.IsEnabled = false;
                BtnSetUpPostions.IsEnabled = false;
            }
            else if (comissioner.IsPresident)
            {
                BtnSetUpElection.IsEnabled = false;
                BtnCreateAccount.IsEnabled = false;
                BtnSetUpCandidates.IsEnabled = false;
                BtnSetUpPostions.IsEnabled = false;
                BtnSetUpVoters.IsEnabled = false;
            }
            else if (!comissioner.IsChairman && !comissioner.IsAdmin && !comissioner.IsPresident)
            {
                BtnSetUpElection.IsEnabled = false;
                BtnCreateAccount.IsEnabled = false;
                BtnSetUpCandidates.IsEnabled = false;
                BtnSetUpPostions.IsEnabled = false;
                BtnDeclareVotes.IsEnabled = false;
            }
        }

        private void BtnDeclareVotes_Click(object? sender, RoutedEventArgs e)
        {
            _sp.GetRequiredService<PresidentLoginWindow>().Show();
        }

        private void BtnCreateAccount_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminCreateAccountPage>());
        }

        private void BtnSetUpElection_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminSetUpElectionPage>());
        }

        private void BtnSetUpPostions_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminSetUpPositionPage>());
        }

        private void BtnSetUpCandidates_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminSetUpCandidatesPage>());
        }

        private void BtnSetUpVoters_Click(object? sender, RoutedEventArgs e)
        {
            MainWindow.Navigate?.Invoke(_sp.GetRequiredService<AdminAddVotersPage>());
        }
    }
}
