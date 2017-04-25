using System.Windows;
using MahApps.Metro.Controls;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
	/// <summary>
	/// Interaction logic for ECChairmanLoginWindow.xaml
	/// </summary>
	public partial class EcChairmanLoginWindow : MetroWindow
	{
		public EcChairmanLoginWindow()
		{
			InitializeComponent();
			WindowState = WindowState.Maximized;
			BtnLogin.Click += BtnLogin_Click;
		}

		private async void BtnLogin_Click(object sender, RoutedEventArgs e)
		{

			if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
			{
				var chairman = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password, IsChairman = true });
				if (chairman != null)
				{
					new ReportViewerWindow().Show();
					Close();
				}
			}
			
			
		}
	}
}
