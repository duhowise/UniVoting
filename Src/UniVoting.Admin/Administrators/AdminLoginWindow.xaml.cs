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
        private readonly IElectionConfigurationService _electionService;
        private readonly IVotingService _votingService;
        private readonly ILogger _logger;

        public AdminLoginWindow()
        {
            InitializeComponent();
            _electionService = null!;
            _votingService = null!;
            _logger = null!;
        }

        public AdminLoginWindow(IElectionConfigurationService electionService, IVotingService votingService, ILogger logger)
        {
            _electionService = electionService;
            _votingService = votingService;
            _logger = logger;
            InitializeComponent();
            BtnLogin.Click += BtnLogin_Click;
        }

        private async void BtnLogin_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Username.Text) && !string.IsNullOrWhiteSpace(Password.Text))
            {
                try
                {
                    var admin = await _electionService.Login(new Comissioner { UserName = Username.Text, Password = Password.Text });
                    if (admin != null)
                    {
                        new MainWindow(admin, _electionService, _votingService, _logger).Show();
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
