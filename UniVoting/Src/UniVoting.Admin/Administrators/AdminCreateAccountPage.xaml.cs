using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
    /// <summary>
    /// Interaction logic for AdminCreateAccountPage.xaml
    /// </summary>
    public partial class AdminCreateAccountPage : Page
    {
        public AdminCreateAccountPage()
        {
            InitializeComponent();
            BtnSave.Click += BtnSave_Click;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxName.Text)||!string.IsNullOrWhiteSpace(Password.Text))
            {
                var metroWindow = (Window.GetWindow(this) as MetroWindow);
                try
                {
                    new ElectionConfigurationService().AddUser(new User
                    {
                        FullName = TextBoxName.Text,
                        UserName =Username.Text,
                        Password = Password.Text
                    });
                    await metroWindow.ShowMessageAsync("Success !", "Action Successful");
                    Util.Clear(this);
                    TextBoxName.Focus();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    await metroWindow.ShowMessageAsync("Wait !", "Something Went Wrong");

                }
            }

        }
    }
}
