using System.Windows;
using System.Windows.Controls;
using Autofac;
using UniVoting.Admin.Startup;
using UniVoting.Core;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for AdminMenuPage.xaml
	/// </summary>
	public partial class AdminMenuPage : Page
	{
		public Commissioner Commissioner { get; }

	    private IContainer container;
	   
        public AdminMenuPage(Commissioner commissioner)
		{
			Commissioner = commissioner;

            container=new BootStrapper().BootStrap();
			InitializeComponent();
			BtnDeclareVotes.Click += BtnDeclareVotes_Click;
			BtnDispensePassword.Click += BtnDispensePassword_Click;
            Loaded += AdminMenuPage_Loaded;
            BtnAddFaculty.Click += BtnAddFaculty_Click;
		}

        private void BtnAddFaculty_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnDispensePassword_Click(object sender, RoutedEventArgs e)
		{
		    container.Resolve<AdminDispensePasswordWindow>().ShowDialog()
		        ;
			//new AdminDispensePasswordWindow(TODO).ShowDialog();
		}

		private void AdminMenuPage_Loaded(object sender, RoutedEventArgs e)
		{

			if (Commissioner.IsChairman)
			{
				BtnSetUpElection.IsEnabled = false;
				BtnCreateAccount.IsEnabled = false;
				BtnSetUpCandidates.IsEnabled = false;
				BtnSetUpPostions.IsEnabled = false;
                BtnAddFaculty.IsEnabled = false;
			
			}
			else if(Commissioner.IsPresident)
			{
				BtnSetUpElection.IsEnabled = false;
				BtnCreateAccount.IsEnabled = false;
				BtnSetUpCandidates.IsEnabled = false;
				BtnSetUpPostions.IsEnabled = false;
				BtnSetUpVoters.IsEnabled = false;
                BtnAddFaculty.IsEnabled = false;
			}
			else if (!Commissioner.IsChairman && !Commissioner.IsAdmin && !Commissioner.IsPresident)
			{
				BtnSetUpElection.IsEnabled = false;
				BtnCreateAccount.IsEnabled = false;
				BtnSetUpCandidates.IsEnabled = false;
				BtnSetUpPostions.IsEnabled = false;
                BtnAddFaculty.IsEnabled = false;
				BtnDeclareVotes.IsEnabled = false;
			}
		}

		private void BtnDeclareVotes_Click(object sender, RoutedEventArgs e)
		{
		    container.Resolve<PresidentLoginWindow>().ShowDialog();

			//new PresidentLoginWindow(TODO).ShowDialog();
		}

		private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
		{
		    var page = container.Resolve<AdminCreateAccountPage>();
		   
              NavigationService?.Navigate(page);
			
		}

		private void BtnSetUpElection_Click(object sender, RoutedEventArgs e)
		{
		    var page = container.Resolve<AdminSetUpElectionPage>();

		    NavigationService?.Navigate(page);
			
		}

		private void BtnSetUpPostions_Click(object sender, RoutedEventArgs e)
		{
		    var page = container.Resolve<AdminSetUpPositionPage>();

		    NavigationService?.Navigate(page);

			
		}

		private void BtnSetUpCandidates_Click(object sender, RoutedEventArgs e)
		{
		    var page = container.Resolve<AdminSetUpCandidatesPage>();

		    NavigationService?.Navigate(page);


            //NavigationService?.Navigate(new AdminSetUpCandidatesPage());
			
		}

		private void BtnSetUpVoters_Click(object sender, RoutedEventArgs e)
		{
		    var page = container.Resolve<AdminAddVotersPage>();

		    NavigationService?.Navigate(page);
            //NavigationService?.Navigate(new AdminAddVotersPage(TODO, TODO));
		}
	}
}
