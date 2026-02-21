using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class PresidentLoginWindow : Window
    {
        private readonly IElectionConfigurationService _electionService;
        private readonly IServiceProvider _sp;

        public PresidentLoginWindow()
        {
            InitializeComponent();
            _electionService = null!;
            _sp = null!;
        }

        public PresidentLoginWindow(IElectionConfigurationService electionService, IServiceProvider sp)
        {
            _electionService = electionService;
            _sp = sp;
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
                    _sp.GetRequiredService<EcChairmanLoginWindow>().Show();
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
