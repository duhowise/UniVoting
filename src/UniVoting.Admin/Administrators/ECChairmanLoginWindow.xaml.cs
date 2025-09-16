using System.Windows;
using UniVoting.Model;
using UniVoting.Services;
using Wpf.Ui.Controls;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for ECChairmanLoginWindow.xaml
	/// </summary>
	public partial class EcChairmanLoginWindow : FluentWindow
	{
		private readonly ElectionConfigurationService _electionConfigurationService;

		public EcChairmanLoginWindow(ElectionConfigurationService electionConfigurationService)
		{
			InitializeComponent();
			_electionConfigurationService = electionConfigurationService;
			
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
					var reportWindow = App.GetService<ReportViewerWindow>();
					reportWindow.Show();
					Close();
				}
				else
				{
					var dialog = new ContentDialog
					{
						Title = "Login Error",
						Content = "Wrong username or password.",
						PrimaryButtonText = "Ok"
					};
					await dialog.ShowAsync();
					Util.Clear(this);
					BtnLogin.IsEnabled = true;
					Username.Focus();

				}
			}
			else
			{
				var dialog = new ContentDialog
				{
					Title = "Login Error", 
					Content = "Wrong username or password.",
					PrimaryButtonText = "Ok"
				};
				await dialog.ShowAsync();
				Util.Clear(this);
				Username.Focus();

			}


		}
	}
}
