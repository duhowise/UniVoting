using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Admin.Startup;
using UniVoting.Core;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for ECChairmanLoginWindow.xaml
	/// </summary>
	public partial class EcChairmanLoginWindow : MetroWindow
	{
		private readonly IElectionConfigurationService _electionConfigurationService;

		public EcChairmanLoginWindow(IElectionConfigurationService electionConfigurationService)
		{
            var container = new BootStrapper().BootStrap();
            _electionConfigurationService = electionConfigurationService;

            InitializeComponent();
			WindowState = WindowState.Maximized;
			BtnLogin.IsDefault = true;
			BtnLogin.Click += BtnLogin_Click;
			Username.Focus();
		}

		private async void BtnLogin_Click(object sender, RoutedEventArgs e)
		{

			if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
			{
				var chairman = await _electionConfigurationService.Login(new Commissioner { UserName = Username.Text, Password = Password.Password, IsChairman = true });
				if (chairman != null)
				{
					new ReportViewerWindow().Show();
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
				Username.Focus();

			}


		}
	}
}
