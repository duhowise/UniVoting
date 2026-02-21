using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using MsBox.Avalonia;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
    public partial class AdminCreateAccountPage : UserControl
    {
        private readonly IElectionConfigurationService _electionService;

        /// <summary>Required by Avalonia's XAML runtime loader. Do not use in application code.</summary>
        public AdminCreateAccountPage()
        {
            throw new NotSupportedException("This constructor is required by Avalonia's XAML runtime loader and must not be called directly.");
        }

        public AdminCreateAccountPage(IElectionConfigurationService electionService)
        {
            _electionService = electionService;
            InitializeComponent();
            BtnSave.Click += BtnSave_Click;
            IsChairman.IsCheckedChanged += IsChairman_Checked;
            IsPresident.IsCheckedChanged += IsPresident_Checked;
            RepeatPassword.TextChanged += RepeatPassword_PasswordChanged;
        }

        private void RepeatPassword_PasswordChanged(object? sender, TextChangedEventArgs e)
        {
            var pwd = Password.Text ?? string.Empty;
            var repeat = RepeatPassword.Text ?? string.Empty;
            RepeatPassword.Foreground = !pwd.Equals(repeat)
                ? new SolidColorBrush(Colors.OrangeRed) : Password.Foreground;
        }

        private void IsPresident_Checked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            IsChairman.IsChecked = false;
        }

        private void IsChairman_Checked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            IsPresident.IsChecked = false;
        }

        private async void BtnSave_Click(object? sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxName.Text) || !string.IsNullOrWhiteSpace(Password.Text))
            {
                try
                {
                    await _electionService.SaveComissioner(new Comissioner
                    {
                        FullName = TextBoxName.Text,
                        UserName = Username.Text,
                        Password = Password.Text,
                        IsChairman = Convert.ToBoolean(IsChairman.IsChecked),
                        IsPresident = Convert.ToBoolean(IsPresident.IsChecked)
                    });
                    await MessageBoxManager.GetMessageBoxStandard("Success!", $"{Username.Text} Successfully created").ShowAsync();
                    Util.Clear(this);
                    TextBoxName.Focus();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    await MessageBoxManager.GetMessageBoxStandard("Wait!", "Something Went Wrong").ShowAsync();
                }
            }
        }
    }
}
