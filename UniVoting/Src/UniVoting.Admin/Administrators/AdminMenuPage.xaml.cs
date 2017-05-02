using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;

namespace UniVoting.WPF.Administrators
{
	/// <summary>
	/// Interaction logic for AdminMenuPage.xaml
	/// </summary>
	public partial class AdminMenuPage : Page
	{
		public Comissioner Comissioner { get; }

		public AdminMenuPage(Comissioner comissioner)
		{
			Comissioner = comissioner;
			InitializeComponent();
			BtnDeclareVotes.Click += BtnDeclareVotes_Click;
			Loaded += AdminMenuPage_Loaded;
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
			new PresidentLoginWindow().ShowDialog();
		}

		private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
		{
			  NavigationService?.Navigate(new AdminCreateAccountPage());
			
		}

		private void BtnSetUpElection_Click(object sender, RoutedEventArgs e)
		{
			
				NavigationService?.Navigate(new AdminSetUpElectionPage());
			
		}

		private void BtnSetUpPostions_Click(object sender, RoutedEventArgs e)
		{
			
				NavigationService?.Navigate(new AdminSetUpPositionPage());
			
		}

		private void BtnSetUpCandidates_Click(object sender, RoutedEventArgs e)
		{
			
				NavigationService?.Navigate(new AdminSetUpCandidatesPage());
			
		}

		private void BtnSetUpVoters_Click(object sender, RoutedEventArgs e)
		{
			NavigationService?.Navigate(new AdminAddVotersPage());
		}
	}
}
