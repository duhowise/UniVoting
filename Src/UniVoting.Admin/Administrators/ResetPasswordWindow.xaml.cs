using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class ResetPasswordWindow : Window
    {
        private readonly IElectionConfigurationService _electionService;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public ResetPasswordWindow()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public ResetPasswordWindow(IElectionConfigurationService electionService)
        {
            _electionService = electionService;
            InitializeComponent();
            BtnReset.Click += BtnReset_Click;
        }

        private async void BtnReset_Click(object? sender, RoutedEventArgs e)
        {
            var username = Username.Text ?? string.Empty;
            var fullName = FullName.Text ?? string.Empty;
            var newPassword = NewPassword.Text ?? string.Empty;
            var confirmPassword = ConfirmPassword.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(fullName))
            {
                await MessageBoxManager.GetMessageBoxStandard("Reset Error", "Please enter your username and full name.").ShowAsync();
                return;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                await MessageBoxManager.GetMessageBoxStandard("Reset Error", "Please enter a new password.").ShowAsync();
                return;
            }

            if (!newPassword.Equals(confirmPassword, StringComparison.Ordinal))
            {
                await MessageBoxManager.GetMessageBoxStandard("Reset Error", "Passwords do not match.").ShowAsync();
                return;
            }

            try
            {
                var success = await _electionService.ResetPasswordAsync(username, fullName, newPassword);
                if (success)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Success", "Password has been reset successfully. Please log in with your new password.").ShowAsync();
                    Close();
                }
                else
                {
                    await MessageBoxManager.GetMessageBoxStandard("Reset Error", "Username or full name does not match any account.").ShowAsync();
                    Util.Clear(this);
                    Username.Focus();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await MessageBoxManager.GetMessageBoxStandard("Connection Error", "Could not connect to the database. Please try again later.").ShowAsync();
            }
        }
    }
}
