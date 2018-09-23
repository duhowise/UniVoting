using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for PresidentLoginWindow.xaml
	/// </summary>
	public partial class PresidentLoginWindow : MetroWindow
	{
		private readonly IElectionConfigurationService _electionConfigurationService;

		public PresidentLoginWindow(IElectionConfigurationService electionConfigurationService)
		{
			_electionConfigurationService = electionConfigurationService;
			InitializeComponent();
			WindowState=WindowState.Maximized;
			BtnLogin.IsDefault = true;
			BtnLogin.Click += BtnLogin_Click;
			Username.Focus();
		}

		private async void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
			{
				BtnLogin.IsEnabled = false;
				var president = await _electionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password, IsPresident = true });
				
				if (president != null)
				{
					//interface
					new EcChairmanLoginWindow(TODO).Show();
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
