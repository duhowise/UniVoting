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
        private CustomDialog _customDialog;
        private AddPositionDialogControl addPositionDialogControl;

        public AdminSetUpPositionPage()
		{
			InitializeComponent();
		
		   Instance = this;
			Instance.Loaded += Instance_Loaded;
			
			
		}
		
		private async void Instance_Loaded(object sender, RoutedEventArgs e)
		{
			PositionControlHolder.Children.Clear();
		   var positions =await ElectionConfigurationService.GetAllPositionsAsync();
			foreach (var position in positions)
				PositionControlHolder.Children.Add(new PositionControl
				{
					TextBoxPosition = {Text = position.PositionName},
					Id = position.Id
				});
            
            _customDialog = new CustomDialog();
            addPositionDialogControl = new AddPositionDialogControl();
            addPositionDialogControl.BtnCancel.Click += BtnSaveClick;
            addPositionDialogControl.BtnSave.Click += BtnCancelClick;
            _customDialog.Content = addPositionDialogControl;
           
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
			await metroWindow.ShowMetroDialogAsync(_customDialog,settings);
	
		}

		public void RemovePosition(UserControl c)
		{
			PositionControlHolder.Children.Remove(c);
		}

        private void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            var pos = addPositionDialogControl.TextBoxPosition.Text;
            var fac = addPositionDialogControl.TextBoxFaculty.Text;
            PositionControlHolder.Children.Add(new PositionControl(pos));
        }
        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            _customDialog._WaitForCloseAsync();
        }
    }
}
