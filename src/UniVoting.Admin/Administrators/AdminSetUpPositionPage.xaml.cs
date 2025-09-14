using Microsoft.Extensions.Logging;
using System;
using System.Windows;
using System.Windows.Controls;
using UniVoting.Model;
using UniVoting.Services;
using Wpf.Ui.Controls;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	///     Interaction logic for AdminSetUpPositionPage.xaml
	/// </summary>
	public partial class AdminSetUpPositionPage : Page
	{
        private readonly ILogger<AdminSetUpPositionPage> _logger;
		public static AdminSetUpPositionPage Instance;
        private ContentDialog _contentDialog;
        private AddPositionDialogControl _addPositionDialogControl;
        private readonly ElectionConfigurationService _electionConfigurationService;

        public AdminSetUpPositionPage(ElectionConfigurationService electionConfigurationService, ILogger<AdminSetUpPositionPage> logger)
		{
			InitializeComponent();
			_electionConfigurationService = electionConfigurationService;
			_logger = logger;
		
		   Instance = this;
			Instance.Loaded += Instance_Loaded;
			
			
		}
		
		private async void Instance_Loaded(object sender, RoutedEventArgs e)
		{
			PositionControlHolder.Children.Clear();
		    try
		    {
		        var positions = await _electionConfigurationService.GetAllPositionsAsync();
		        if (positions!=null)
		        {
		            foreach (var position in positions)
		                PositionControlHolder.Children.Add(new PositionControl(_electionConfigurationService)
		                {
		                    TextBoxPosition = { Text = position.PositionName },
		                    TextBoxFaculty = { Text = position.Faculty },
		                    Id = position.Id
		                });
                }
		        
            }
		    catch (Exception exception)
		    {
		        var errorDialog = new ContentDialog
		        {
		            Title = "Error",
		            Content = exception.Message,
		            PrimaryButtonText = "Ok"
		        };
		        await errorDialog.ShowAsync();
                _logger.LogError(exception, "Failed to load positions");
		    }
		  

            _addPositionDialogControl = new AddPositionDialogControl();
            _addPositionDialogControl.BtnCancel.Click +=BtnCancelClick;
            _addPositionDialogControl.BtnSave.Click += BtnSaveClick;
            
            _contentDialog = new ContentDialog
            {
                Title = "Add Position",
                Content = _addPositionDialogControl,
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Cancel"
            };
           
        }


		private async void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			await _contentDialog.ShowAsync();
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
            PositionControlHolder.Children.Add(new PositionControl(_electionConfigurationService, pos));
            _contentDialog.Hide();
        }
        
        private async void BtnCancelClick(object sender, RoutedEventArgs e)
        {
          _contentDialog.Hide();
        }
    }
}
