using System.Windows;
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
            BtnLogin.IsDefault = true;
            BtnLogin.Click += BtnLogin_Click;
            Username.Focus();
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
                else
                {
                    MessageBox.Show("Wrong username or password.", "Login Error");
                    Util.Clear(this);
                    BtnLogin.IsEnabled = true;
                    Username.Focus();
                }
            }
            else
            {
                MessageBox.Show("Wrong username or password.", "Login Error");
                Util.Clear(this);
                Username.Focus();
            }
        }
    }
}
