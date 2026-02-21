using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class EcChairmanLoginWindow : Window
    {
        private readonly IElectionConfigurationService _electionService;
        private readonly IServiceProvider _sp;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public EcChairmanLoginWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public EcChairmanLoginWindow(IElectionConfigurationService electionService, IServiceProvider sp)
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
                var chairman = await _electionService.Login(new Comissioner { UserName = Username.Text, Password = Password.Text, IsChairman = true });
                if (chairman != null)
                {
                    _sp.GetRequiredService<ReportViewerWindow>().Show();
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
