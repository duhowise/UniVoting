using System.Windows;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminLoginWindow : Window
    {
        public AdminLoginWindow()
        {
            InitializeComponent();
            BtnLogin.IsDefault = true;
            Username.Focus();
            BtnLogin.Click += BtnLogin_Click;
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Password))
            {
                var admin = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password });
                if (admin != null)
                {
                    new MainWindow(admin).Show();
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
                MessageBox.Show("Please Enter Username or password to Login.", "Login Error");
                Util.Clear(this);
                Username.Focus();
            }
        }
    }
}
