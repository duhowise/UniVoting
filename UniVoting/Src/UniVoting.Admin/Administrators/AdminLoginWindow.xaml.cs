using MahApps.Metro.Controls;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
	
	/// <summary>
	/// Interaction logic for AdminLoginWindow.xaml
	/// </summary>
	public partial class AdminLoginWindow : MetroWindow
	{
        private ILogger _logger=new SystemEventLoggerService();
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
                try
                {
                    var admin = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password });
                    if (admin != null)
                    {
                        new MainWindow(admin).Show();
                        Close();
                    }
                }
                catch (System.Exception exception)
                {
                    await this.ShowMessageAsync("Login Error", $"{exception.Message}");
                    Util.Clear(this);
                    BtnLogin.IsEnabled = true;
                    Username.Focus();
                    _logger.Log(exception);
                    
                }
                await this.ShowMessageAsync("Login Error", "Wrong username or password.");
			    Util.Clear(this);
			    BtnLogin.IsEnabled = true;
			    Username.Focus();
			   
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
