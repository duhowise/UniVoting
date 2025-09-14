using Microsoft.Extensions.Logging;
using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for AdminMenuPage.xaml
	/// </summary>
	public partial class AdminMenuPage : Page
	{
		public Comissioner Comissioner { get; }
		private readonly VotingService _votingService;
		private readonly ElectionConfigurationService _electionConfigurationService;
		private readonly ILoggerFactory _loggerFactory;

		public AdminMenuPage(Comissioner comissioner, VotingService votingService, ElectionConfigurationService electionConfigurationService, ILoggerFactory loggerFactory)
		{
			Comissioner = comissioner;
			_votingService = votingService;
			_electionConfigurationService = electionConfigurationService;
			_loggerFactory = loggerFactory;
			
			InitializeComponent();
			BtnDeclareVotes.Click += BtnDeclareVotes_Click;
			BtnDispensePassword.Click += BtnDispensePassword_Click;
			Loaded += AdminMenuPage_Loaded;
		}

		private void BtnDispensePassword_Click(object sender, RoutedEventArgs e)
		{
			var passwordWindow = App.GetService<AdminDispensePasswordWindow>();
			passwordWindow.ShowDialog();
		}

		private void AdminMenuPage_Loaded(object sender, RoutedEventArgs e)
		{

			if (Comissioner.IsChairman)
			{
				BtnSetUpElection.IsEnabled = false;
				BtnCreateAccount.IsEnabled = false;
				BtnSetUpCandidates.IsEnabled = false;
				BtnSetUpPostions.IsEnabled = false;
			
			}
			else if(Comissioner.IsPresident)
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

		private void BtnDeclareVotes_Click(object sender, RoutedEventArgs e)
		{
			var presidentWindow = App.GetService<PresidentLoginWindow>();
			presidentWindow.ShowDialog();
		}

		private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
		{
			var page = new AdminCreateAccountPage(_electionConfigurationService);
			NavigationService?.Navigate(page);
		}

		private void BtnSetUpElection_Click(object sender, RoutedEventArgs e)
		{
			var page = new AdminSetUpElectionPage(_electionConfigurationService);
			NavigationService?.Navigate(page);
		}

		private void BtnSetUpPostions_Click(object sender, RoutedEventArgs e)
		{
			var logger = _loggerFactory.CreateLogger<AdminSetUpPositionPage>();
			var page = new AdminSetUpPositionPage(_electionConfigurationService, logger);
			NavigationService?.Navigate(page);
		}

		private void BtnSetUpCandidates_Click(object sender, RoutedEventArgs e)
		{
			var logger = _loggerFactory.CreateLogger<AdminSetUpCandidatesPage>();
			var page = new AdminSetUpCandidatesPage(_electionConfigurationService, logger);
			NavigationService?.Navigate(page);
		}

		private void BtnSetUpVoters_Click(object sender, RoutedEventArgs e)
		{
			var page = new AdminAddVotersPage(_votingService, _electionConfigurationService);
			NavigationService?.Navigate(page);
		}
	}
}
