using System;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Services;

namespace UniVoting.WPF.Administrators
{
	/// <summary>
	///     Interaction logic for AdminSetUpPositionPage.xaml
	/// </summary>
	public partial class AdminSetUpPositionPage : Page
	{
		public static AdminSetUpPositionPage Instance;

		public AdminSetUpPositionPage()
		{
			InitializeComponent();
		
		   Instance = this;
			Instance.Loaded += Instance_Loaded;
			
			
		}
		
		private async void Instance_Loaded(object sender, RoutedEventArgs e)
		{
			PositionControlHolder.Children.Clear();
		   var positions =await ElectionConfigurationService.GetAllPositions();
			foreach (var position in positions)
				PositionControlHolder.Children.Add(new PositionControl
				{
					TextBoxPosition = {Text = position.PositionName},
					Id = position.Id
				});
		}


		private async void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			var metroWindow = Window.GetWindow(this) as MetroWindow;
			var settings = new MetroDialogSettings
			{
				ColorScheme = MetroDialogColorScheme.Accented,
				AffirmativeButtonText = "OK",
				AnimateShow = true,
				NegativeButtonText = "Go away!",
				FirstAuxiliaryButtonText = "Cancel"
			};
			var result = await metroWindow.ShowInputAsync("Enter New Position ", "Position Name", settings);
			if (result!=null)
			{
				PositionControlHolder.Children.Add(new PositionControl(result));

			}
		}

		public void RemovePosition(UserControl c)
		{
			PositionControlHolder.Children.Remove(c);
		}
	}
}
