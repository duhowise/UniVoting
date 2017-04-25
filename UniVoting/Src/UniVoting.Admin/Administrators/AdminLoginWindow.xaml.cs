using MahApps.Metro.Controls;
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
			BtnLogin.Click += BtnLogin_Click;
		}

		private async void BtnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
			{
				var admin = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password, IsAdmin = true });
				if (admin != null)
				{
					new MainWindow().Show();
					Close();
				}
			}
		}
	}
}
