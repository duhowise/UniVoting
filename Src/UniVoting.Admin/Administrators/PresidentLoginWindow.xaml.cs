using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Core;
using Univoting.Services;

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
			WindowState=WindowState.Maximized;
			BtnLogin.IsDefault = true;
			BtnLogin.Click += BtnLogin_Click;
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
                    new EcChairmanLoginWindow(_electionConfigurationService).Show();
					BtnLogin.IsEnabled = true;

					Close();
				}
				else
				{
					await this.ShowMessageAsync("Login Error", "Wrong username or password.");
					Util.Clear(this);
					BtnLogin.IsEnabled = true;
					Username.Focus();

				}
			}
			else
			{
				await this.ShowMessageAsync("Login Error", "Wrong username or password.");
				Util.Clear(this);
				BtnLogin.IsEnabled = true;
				Username.Focus();

			}



		}
	}
}
