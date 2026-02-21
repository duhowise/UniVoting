using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminMenuPage : UserControl
    {
        public Comissioner Comissioner { get; }
        private readonly IElectionConfigurationService _electionService;
        private readonly IVotingService _votingService;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminMenuPage()
        {
            InitializeComponent();
            Comissioner = new Comissioner();
            _electionService = null!;
            _votingService = null!;
            _sp = null!;
        }

        public AdminMenuPage(Comissioner comissioner, IElectionConfigurationService electionService, IVotingService votingService, IServiceProvider sp)
        {
            Comissioner = comissioner;
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
            if (Comissioner.IsChairman)
            {
                BtnSetUpElection.IsEnabled = false;
                BtnCreateAccount.IsEnabled = false;
                BtnSetUpCandidates.IsEnabled = false;
                BtnSetUpPostions.IsEnabled = false;
            }
            else if (Comissioner.IsPresident)
            {
                BtnSetUpElection.IsEnabled = false;
                BtnCreateAccount.IsEnabled = false;
                BtnSetUpCandidates.IsEnabled = false;
                BtnSetUpPostions.IsEnabled = false;
                BtnSetUpVoters.IsEnabled = false;
            }
            else if (!Comissioner.IsChairman && !Comissioner.IsAdmin && !Comissioner.IsPresident)
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
