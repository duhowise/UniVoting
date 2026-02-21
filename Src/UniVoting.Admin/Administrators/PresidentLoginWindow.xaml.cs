using System.Windows;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class PresidentLoginWindow : Window
    {
        public PresidentLoginWindow()
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
                BtnLogin.IsEnabled = false;
                var president = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Password, IsPresident = true });
                if (president != null)
                {
                    new EcChairmanLoginWindow().Show();
                    BtnLogin.IsEnabled = true;
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
                BtnLogin.IsEnabled = true;
                Username.Focus();
            }
        }
    }
}
