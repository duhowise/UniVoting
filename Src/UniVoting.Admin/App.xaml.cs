using System;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro;
using UniVoting.Admin.Administrators;
using UniVoting.Admin.StatUp;

namespace UniVoting.Admin
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			var bootstrapper = new BootStrapper();
			var container = bootstrapper.BootStrap();
			// add custom accent and theme resource dictionaries
			ThemeManager.AddAccent("CustomAccent1", new Uri("pack://application:,,,/UniVoting.Admin;component/CustomAccents/CustomAccent.xaml"));

			// create custom accents
			ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
			ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
			ThemeManagerHelper.CreateAppStyleBy(Colors.Indigo, true);



			MainWindow=new AdminLoginWindow();
			MainWindow.Show();
			base.OnStartup(e);
		}

		private void App_OnStartup(object sender, StartupEventArgs e)
		{
		   
		}
	}
}
