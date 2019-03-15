using System.Windows;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Admin.Startup;
using UniVoting.Core;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for PresidentLoginWindow.xaml
	/// </summary>
	public partial class PresidentLoginWindow : MetroWindow
	{
		private readonly IElectionConfigurationService _electionConfigurationService;
	    private readonly IVotingService _votingService;

	    public PresidentLoginWindow(IElectionConfigurationService electionConfigurationService,IVotingService votingService)
		{
			_electionConfigurationService = electionConfigurationService;
		    _votingService = votingService;
		    InitializeComponent();
			BtnLogin.Click += BtnLogin_Click;
			//Username.Focus();
		}

		private async void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
			{
				BtnLogin.IsEnabled = false;
				var president = await _electionConfigurationService.LoginAsync(new Commissioner { UserName = Username.Text, Password = Password.Password, IsPresident = true });
				
				if (president != null)
				{

                    //interface
                    var container = new BootStrapper().BootStrap();
                    var window = container.Resolve<EcChairmanLoginWindow>();
                    window.ShowDialog();
					//BtnLogin.IsEnabled = true;
                    Close();
				}
				else
				{
					await this.ShowMessageAsync("LoginAsync Error", "Wrong username or password.");
					Util.Clear(this);
					BtnLogin.IsEnabled = true;

				}
			}
			else
			{
				await this.ShowMessageAsync("LoginAsync Error", "Wrong username or password.");
				Util.Clear(this);
				BtnLogin.IsEnabled = true;

			}



		}
	}
}
