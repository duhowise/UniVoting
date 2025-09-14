using Microsoft.Extensions.Logging;
using UniVoting.Model;
using UniVoting.Services;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;
using MessageBoxImage = System.Windows.MessageBoxImage;

namespace UniVoting.Admin.Administrators
{
	
	/// <summary>
	/// Interaction logic for AdminLoginWindow.xaml
	/// </summary>
	public partial class AdminLoginWindow : FluentWindow
	{
        private readonly ILogger<AdminLoginWindow> _logger;
        private readonly ElectionConfigurationService _electionConfigurationService;

		public AdminLoginWindow(ILogger<AdminLoginWindow> logger, ElectionConfigurationService electionConfigurationService)
		{

			InitializeComponent();
            ExtendsContentIntoTitleBar = true;
            _logger = logger;
			_electionConfigurationService = electionConfigurationService;
			
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
                    var admin = await _electionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password, IsAdmin = true });
                    if (admin != null)
                    {
                        var mainWindow = App.GetService<MainWindow>();
                        mainWindow.Initialize(admin);
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Wrong username or password.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        Util.Clear(this);
                        BtnLogin.IsEnabled = true;
                        Username.Focus();
                    }
                }
                catch (System.Exception exception)
                {
                    MessageBox.Show($"Login Error: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Util.Clear(this);
                    BtnLogin.IsEnabled = true;
                    Username.Focus();
                    _logger.LogError(exception, "Login failed for username {Username}", Username.Text);
                }
            }
			else
			{
				MessageBox.Show("Please Enter Username or password to Login.", "Login Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				Util.Clear(this);
				Username.Focus();
			}
		}
	}
}
