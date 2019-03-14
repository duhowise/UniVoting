using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Admin.Startup;
using UniVoting.Services;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	///     Interaction logic for AdminSetUpPositionPage.xaml
	/// </summary>
	public partial class AdminSetUpPositionPage : Page
	{
		private readonly IElectionConfigurationService _electionConfigurationService;
		public static AdminSetUpPositionPage Instance;
        private CustomDialog _customDialog;
        private AddPositionDialogControl _addPositionDialogControl;
	    private MetroWindow _metroWindow;

        public AdminSetUpPositionPage(IElectionConfigurationService electionConfigurationService)
		{
            _electionConfigurationService = electionConfigurationService;
            var container = new BootStrapper().BootStrap();
			InitializeComponent();
		
		   Instance = this;
			Instance.Loaded += Instance_Loaded;
			
			
		}
		
		private async void Instance_Loaded(object sender, RoutedEventArgs e)
		{
			PositionControlHolder.Children.Clear();
		   var positions =await _electionConfigurationService.GetAllPositionsAsync();
           if (positions.Count>0)
           {
               foreach (var position in positions)
                   PositionControlHolder.Children.Add(new PositionControl
                   {
                       TextBoxPosition = {Text = position.PositionName},
                       TextBoxFaculty = {Text =position.Faculty },
                       Id = position.Id
                   });
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
          await  _electionConfigurationService.AddPosition(new Model.Position{PositionName = pos,Faculty = fac});
            PositionControlHolder.Children.Add(new PositionControl(pos));
            await  _metroWindow.HideMetroDialogAsync(_customDialog);


        }
        private async void BtnCancelClick(object sender, RoutedEventArgs e)
        {
          await  _metroWindow.HideMetroDialogAsync(_customDialog);
        }
    }
}
