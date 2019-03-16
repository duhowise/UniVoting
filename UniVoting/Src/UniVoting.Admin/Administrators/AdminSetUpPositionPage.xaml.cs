using System;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Admin.Startup;
using UniVoting.Core;
using Position = UniVoting.Core.Position;

namespace UniVoting.Admin.Administrators
{
	/// <summary>
	///     Interaction logic for AdminSetUpPositionPage.xaml
	/// </summary>
	public partial class AdminSetUpPositionPage : Page
	{
		private IElectionConfigurationService _electionConfigurationService;
	    private MetroWindow _metroWindow;
        private IContainer container;
        public AdminSetUpPositionPage()
		{
            
			InitializeComponent();
             container = new BootStrapper().BootStrap();
            Loaded += AdminSetUpPositionPage_Loaded;
            AddPosition.Click += AddPosition_Click;
			
			
		}

        private async void AddPosition_Click(object sender, RoutedEventArgs e)
        {
            _metroWindow = Window.GetWindow(this) as MetroWindow;
            var settings = new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Theme,
                AnimateShow = true,
            };

            if (string.IsNullOrWhiteSpace(TextBoxPositionName.Text)||string.IsNullOrWhiteSpace(FacultyList.Text))
            {
            await _metroWindow.ShowMessageAsync("Add new Position", "Please Specify Position Name ");

            }

            var result = await _metroWindow.ShowMessageAsync("Add new Position", "are you sure you want to add ", MessageDialogStyle.AffirmativeAndNegative, settings);
            if (result != MessageDialogResult.Affirmative) return;
            _electionConfigurationService = container.Resolve<IElectionConfigurationService>();
            await _electionConfigurationService.AddPositionAsync(new Position { PositionName = TextBoxPositionName.Text, FacultyId = Convert.ToInt32(FacultyList.SelectedValue) });
            await _metroWindow.ShowMessageAsync("Add new Position", "success");
            AdminSetUpPositionPage_Loaded(this, e);

        }

        private async void AdminSetUpPositionPage_Loaded(object sender, RoutedEventArgs e)
        {
            _electionConfigurationService =container.Resolve<IElectionConfigurationService>();
            FacultyList.ItemsSource =await  _electionConfigurationService.GetFacultiesAsync();
            PositionList.ItemsSource
                = await  _electionConfigurationService.GetAllPositionsAsync(true);
        }
        
    }
}
