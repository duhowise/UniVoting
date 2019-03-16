using System;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Univoting.Services;
using UniVoting.Admin.Startup;
using UniVoting.Core;
using UniVoting.Services;
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
			
			
		}

        private void AdminSetUpPositionPage_Loaded(object sender, RoutedEventArgs e)
        {
            _electionConfigurationService =container.Resolve<IElectionConfigurationService>();
            FacultyList.ItemsSource =  _electionConfigurationService.GetFacultiesAsync().Result;
        }

       

        private async void BtnAdd_Click(object sender, RoutedEventArgs e)
		{
			 _metroWindow = Window.GetWindow(this) as MetroWindow;
            var settings = new MetroDialogSettings
			{
				ColorScheme = MetroDialogColorScheme.Theme,
			    AnimateShow = true,
		    };
			await _metroWindow.ShowMessageAsync("","",MessageDialogStyle.AffirmativeAndNegative,settings);
	
		}

		

    }
}
