using System.Windows;
using MahApps.Metro.Controls;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
	/// <summary>
	/// Interaction logic for PresidentLoginWindow.xaml
	/// </summary>
	public partial class PresidentLoginWindow : MetroWindow
	{
		public PresidentLoginWindow()
		{
			InitializeComponent();
			WindowState=WindowState.Maximized;
			BtnLogin.Click += BtnLogin_Click;
		}

		private async void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
			{
				var president = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password, IsPresident = true });
				if (president != null)
				{
					new EcChairmanLoginWindow().Show();
					Close();
				}
			}
			
			

		}
	}
}
