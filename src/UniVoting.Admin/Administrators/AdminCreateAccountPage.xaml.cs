using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UniVoting.Model;
using UniVoting.Services;
using Wpf.Ui.Controls;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	/// Interaction logic for AdminCreateAccountPage.xaml
	/// </summary>
	public partial class AdminCreateAccountPage : Page
	{
		private readonly ElectionConfigurationService _electionConfigurationService;

		public AdminCreateAccountPage(ElectionConfigurationService electionConfigurationService)
		{
			InitializeComponent();
			_electionConfigurationService = electionConfigurationService;
			
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
				try
				{
					await _electionConfigurationService.SaveComissioner(new Commissioner
					{
						FullName = TextBoxName.Text,
						UserName = Username.Text,
						Password = Password.Password,
						IsChairman = Convert.ToBoolean(IsChairman.IsChecked),
						IsPresident = Convert.ToBoolean(IsPresident.IsChecked)
						
					});
					
					var successDialog = new ContentDialog
					{
						Title = "Success!",
						Content = $"{Username.Text} Successfully created",
						PrimaryButtonText = "Ok"
					};
					await successDialog.ShowAsync();
					
					Util.Clear(this);
					TextBoxName.Focus();
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
					var errorDialog = new ContentDialog
					{
						Title = "Wait!",
						Content = "Something Went Wrong",
						PrimaryButtonText = "Ok"
					};
					await errorDialog.ShowAsync();
				}
			}

		}
	}
}
