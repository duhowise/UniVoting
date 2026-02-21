using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class EcChairmanLoginWindow : Window
    {
        public EcChairmanLoginWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            BtnLogin.Click += BtnLogin_Click;
            Username.Focus();
        }

        private async void BtnLogin_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {
                var chairman = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Text, IsChairman = true });
                if (chairman != null)
                {
                    new ReportViewerWindow().Show();
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
                Username.Focus();
            }
        }
    }
}
