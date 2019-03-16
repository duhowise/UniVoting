using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Admin.Startup;
using UniVoting.Core;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for AdminCreateAccountPage.xaml
	/// </summary>
	public partial class AdminCreateAccountPage : Page
	{
		private readonly IElectionConfigurationService _electionConfigurationService;

		public AdminCreateAccountPage(IElectionConfigurationService electionConfigurationService)
		{
			var container = new BootStrapper().BootStrap();
		    _electionConfigurationService = electionConfigurationService;
			InitializeComponent();
			BtnSave.Click += BtnSave_Click;
			IsChairman.Checked += IsChairman_Checked;
			IsPresident.Checked += IsPresident_Checked;
			//RepeatPassword.PasswordChanged += RepeatPassword_TextChanged;
			RepeatPassword.PasswordChanged += RepeatPassword_PasswordChanged;

		}

		private void RepeatPassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			RepeatPassword.Foreground = !Password.Password.Equals(RepeatPassword.Password) ? new SolidColorBrush(Colors.OrangeRed) : Password.Foreground;

		}

		//private void RepeatPassword_TextChanged(object sender, TextChangedEventArgs e)
		//{
		//	RepeatPassword.Foreground = !Password.Text.Equals(RepeatPassword.Text) ? new SolidColorBrush(Colors.OrangeRed) : Password.Foreground;
		//}

		
		
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
				var metroWindow = (Window.GetWindow(this) as MetroWindow);
				try
				{
					await _electionConfigurationService.SaveCommissionerAsync(new Commissioner
					{
						FullName = TextBoxName.Text,
						UserName = Username.Text,
						Password = Password.Password,
						IsChairman = Convert.ToBoolean(IsChairman.IsChecked),
						IsPresident = Convert.ToBoolean(IsPresident.IsChecked)
						
					});
					await metroWindow.ShowMessageAsync("Success !", $"{Username.Text} Successfully created");
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
