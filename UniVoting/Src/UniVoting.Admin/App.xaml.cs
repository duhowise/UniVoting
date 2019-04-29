using System;
using System.Windows;
using System.Windows.Media;
using Autofac;
using MahApps.Metro;
using UniVoting.Admin.Administrators;
using UniVoting.Admin.Startup;

namespace UniVoting.Admin
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			
			// add custom accent and theme resource dictionaries
			ThemeManager.AddAccent("CustomAccent1", new Uri("pack://application:,,,/UniVoting.Admin;component/CustomAccents/CustomAccent.xaml"));

            // create custom accents
            ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
            ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
            ThemeManagerHelper.CreateAppStyleBy(Colors.Indigo, true);

            var bootStrapper = new BootStrapper();
            var container = bootStrapper.BootStrap();
            var window = container.Resolve<AdminLoginWindow>();

            MainWindow = window;
			MainWindow?.Show();
			base.OnStartup(e);
		}

		private void App_OnStartup(object sender, StartupEventArgs e)
		{
		   
		}
	}
}
