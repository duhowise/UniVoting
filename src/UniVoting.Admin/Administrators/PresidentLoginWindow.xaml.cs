using System.Windows;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for PresidentLoginWindow.xaml
	/// </summary>
	public partial class PresidentLoginWindow : FluentWindow
	{
		public PresidentLoginWindow()
		{
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
				// Note: ElectionConfigurationService should be injected as instance service
				// var president = await _electionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password, IsPresident = true });
				
				// For now, commenting out and using MessageBox
				// if (president != null)
				// {
				//     new EcChairmanLoginWindow().Show();
				//     BtnLogin.IsEnabled = true;
				//     Close();
				// }
				// else
				// {
					MessageBox.Show("Login functionality needs dependency injection setup", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
					Util.Clear(this);
					BtnLogin.IsEnabled = true;
					Username.Focus();
				// }
			}
			else
			{
				MessageBox.Show("Wrong username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
				Util.Clear(this);
				BtnLogin.IsEnabled = true;
				Username.Focus();
			}
		}
	}
}
