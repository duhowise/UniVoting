using System;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro;
using UniVoting.WPF.Administrators;
using UniVoting.WPF.StatUp;

namespace UniVoting.WPF
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
			ThemeManager.AddAccent("CustomAccent1", new Uri("pack://application:,,,/UniVoting.WPF;component/CustomAccents/CustomAccent.xaml"));

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
