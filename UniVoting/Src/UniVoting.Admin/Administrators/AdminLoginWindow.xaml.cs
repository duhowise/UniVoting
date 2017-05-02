using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
	/// <summary>
	/// Interaction logic for AdminLoginWindow.xaml
	/// </summary>
	public partial class AdminLoginWindow : MetroWindow
	{
		public AdminLoginWindow()
		{
			InitializeComponent();
			BtnLogin.IsDefault = true;
			Username.Focus();
			BtnLogin.Click += BtnLogin_Click;
			
		}

		private async void BtnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
			{
				var admin = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password});
				if (admin != null)
				{
					new MainWindow(admin).Show();
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
				await this.ShowMessageAsync("Login Error", "Please Enter Username or password to Login.");
				Util.Clear(this);
				Username.Focus();

			}
		}
	}
}
