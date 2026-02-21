using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class PresidentLoginWindow : Window
    {
        private readonly IElectionConfigurationService _electionService;

        public PresidentLoginWindow()
        {
            InitializeComponent();
            _electionService = null!;
        }

        public PresidentLoginWindow(IElectionConfigurationService electionService)
        {
            _electionService = electionService;
            InitializeComponent();
            WindowState = WindowState.Maximized;
            BtnLogin.Click += BtnLogin_Click;
            Username.Focus();
        }

        private async void BtnLogin_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {
                BtnLogin.IsEnabled = false;
                var president = await _electionService.Login(new Comissioner { UserName = Username.Text, Password = Password.Text, IsPresident = true });
                if (president != null)
                {
                    new EcChairmanLoginWindow(_electionService).Show();
                    BtnLogin.IsEnabled = true;
                    Close();
                }
                else
                {
                    await MessageBoxManager.GetMessageBoxStandard("Login Error", "Wrong username or password.").ShowAsync();
                    Util.Clear(this);
                    BtnLogin.IsEnabled = true;
                    Username.Focus();
                }
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Login Error", "Wrong username or password.").ShowAsync();
                Util.Clear(this);
                BtnLogin.IsEnabled = true;
                Username.Focus();
            }
        }
    }
}
