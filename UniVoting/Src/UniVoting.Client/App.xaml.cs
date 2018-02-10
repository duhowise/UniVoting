using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Akavache;
using UniVoting.Client;
using UniVoting.Client.Properties;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client
{
	/// <summary>
	///     Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private IEnumerable<Position> _positions;
		public App()
		{
			BlobCache.ApplicationName = $"VotingApplication";
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

		private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
		    if (e.ExceptionObject is Exception exp) MessageBox.Show(exp.InnerException?.ToString());
		}
	
		protected override void OnStartup(StartupEventArgs e)
		{
			var data = new Setting();
			//if (Settings.Default.FirstRun)
			//{
				GetSettings();
				Settings.Default.FirstRun = false;
				Settings.Default.Save();
			//}

			try
			{
				//get color from  local cache
				data =  BlobCache.UserAccount.GetObject<Setting>("ElectionSettings").Wait();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, " colour Settings Error");
			}

			var rgb = data.Colour.Split(',');
			//ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
		
			ThemeManagerHelper.CreateAppStyleBy(new Color { R = Convert.ToByte(rgb[0]), G = Convert.ToByte(rgb[1]), B = Convert.ToByte(rgb[2]) }, true);
			MainWindow = new ClientsLoginWindow();
			MainWindow.Show();
			base.OnStartup(e);
		}

		private void GetSettings()
		{
			_positions = new List<Position>();

			try
			{
				var electionData = ElectionConfigurationService.ConfigureElection();
				BlobCache.UserAccount.InsertObject("ElectionSettings", electionData).Wait();
				_positions = ElectionConfigurationService.GetAllPositions();

				BlobCache.UserAccount.InsertObject("ElectionPositions", _positions).Wait();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, " Election Settings Error");
			}
		}
	}
}
