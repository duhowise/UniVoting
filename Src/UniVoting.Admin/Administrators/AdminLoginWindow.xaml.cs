using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminLoginWindow : Window
    {
        public AdminLoginWindow()
        {
            InitializeComponent();
            BtnLogin.Click += BtnLogin_Click;
        }

        private async void BtnLogin_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {
                try
                {
                    var admin = await ElectionConfigurationService.Login(new Comissioner { UserName = Username.Text, Password = Password.Text });
                    if (admin != null)
                    {
                        new MainWindow(admin).Show();
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
                catch (Exception ex)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Connection Error", $"Could not connect to the database.\n\n{ex.Message}").ShowAsync();
                }
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Login Error", "Please Enter Username or password to Login.").ShowAsync();
                Util.Clear(this);
                Username.Focus();
            }
        }
    }
}
