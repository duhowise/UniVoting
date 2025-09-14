using System;
using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	///     Interaction logic for AdminSetUpPositionPage.xaml
	/// </summary>
	public partial class AdminSetUpPositionPage : Page
	{
        private ILogger logger=new SystemEventLoggerService();
		public static AdminSetUpPositionPage Instance;
        private CustomDialog _customDialog;
        private AddPositionDialogControl _addPositionDialogControl;
	    private MetroWindow _metroWindow;

        public AdminSetUpPositionPage()
		{
			InitializeComponent();
		
		   Instance = this;
			Instance.Loaded += Instance_Loaded;
			
			
		}
		
		private async void Instance_Loaded(object sender, RoutedEventArgs e)
		{
			PositionControlHolder.Children.Clear();
		    try
		    {
		        var positions = await ElectionConfigurationService.GetAllPositionsAsync();
		        if (positions!=null)
		        {
		            foreach (var position in positions)
		                PositionControlHolder.Children.Add(new PositionControl
		                {
		                    TextBoxPosition = { Text = position.PositionName },
		                    TextBoxFaculty = { Text = position.Faculty },
		                    Id = position.Id
		                });
                }
		        
            }
		    catch (Exception exception)
		    {
		        await _metroWindow.ShowLoginAsync("Error",$"{exception.Message}");

                logger.Log(exception);
		    }
		  
            
            _customDialog = new CustomDialog();
            _addPositionDialogControl = new AddPositionDialogControl();
            _addPositionDialogControl.BtnCancel.Click +=BtnCancelClick;
            _addPositionDialogControl.BtnSave.Click += BtnSaveClick;
            _customDialog.Content = _addPositionDialogControl;
           
        }


		private async void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			 _metroWindow = Window.GetWindow(this) as MetroWindow;
            var settings = new MetroDialogSettings
			{
				ColorScheme = MetroDialogColorScheme.Theme,
			    AnimateShow = true,
		    };
			await _metroWindow.ShowMetroDialogAsync(_customDialog,settings);
	
		}

		public void RemovePosition(UserControl c)
		{
			PositionControlHolder.Children.Remove(c);
		}

        private async void BtnSaveClick(object sender, RoutedEventArgs e)
        {
            var pos = _addPositionDialogControl.TextBoxPosition.Text;
            var fac = _addPositionDialogControl.TextBoxFaculty.Text;
          await  ElectionConfigurationService.AddPosition(new Model.Position{PositionName = pos,Faculty = fac});
            PositionControlHolder.Children.Add(new PositionControl(pos));
            await  _metroWindow.HideMetroDialogAsync(_customDialog);


        }
        private async void BtnCancelClick(object sender, RoutedEventArgs e)
        {
          await  _metroWindow.HideMetroDialogAsync(_customDialog);
        }
    }
}
