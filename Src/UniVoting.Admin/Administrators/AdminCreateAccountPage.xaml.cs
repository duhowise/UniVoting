using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminCreateAccountPage : Page
    {
        public AdminCreateAccountPage()
        {
            InitializeComponent();
            BtnSave.Click += BtnSave_Click;
            IsChairman.Checked += IsChairman_Checked;
            IsPresident.Checked += IsPresident_Checked;
            RepeatPassword.PasswordChanged += RepeatPassword_PasswordChanged;
        }

        private void RepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RepeatPassword.Foreground = !Password.Password.Equals(RepeatPassword.Password)
                ? new SolidColorBrush(Colors.OrangeRed) : Password.Foreground;
        }

        private void IsPresident_Checked(object sender, RoutedEventArgs e)
        {
            IsChairman.IsChecked = false;
        }

        private void IsChairman_Checked(object sender, RoutedEventArgs e)
        {
            IsPresident.IsChecked = false;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxName.Text) || !string.IsNullOrWhiteSpace(Password.Password))
            {
                try
                {
                    await new ElectionConfigurationService().SaveComissioner(new Comissioner
                    {
                        FullName = TextBoxName.Text,
                        UserName = Username.Text,
                        Password = Password.Password,
                        IsChairman = Convert.ToBoolean(IsChairman.IsChecked),
                        IsPresident = Convert.ToBoolean(IsPresident.IsChecked)
                    });
                    MessageBox.Show($"{Username.Text} Successfully created", "Success!");
                    Util.Clear(this);
                    TextBoxName.Focus();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    MessageBox.Show("Something Went Wrong", "Wait!");
                }
            }
        }
    }
}
