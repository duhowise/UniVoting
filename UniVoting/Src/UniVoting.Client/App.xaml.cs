using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Akavache;
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
		    if (e.ExceptionObject is Exception exp) MessageBox.Show(exp.Message);
		}
	
		protected override async void OnStartup(StartupEventArgs e)
		{
			//if (Settings.Default.FirstRun)
			//{
				await GetSettings();
				Settings.Default.FirstRun = false;
				Settings.Default.Save();
			//}
            SetTheme();
		  MainWindow = new ClientsLoginWindow();
			MainWindow.Show();
			base.OnStartup(e);
		}

	    public static async void SetTheme()
	    {
	        var data = new Setting();

	        try
	        {
	            //get color from  local cache
	            data = await BlobCache.UserAccount.GetObject<Setting>("ElectionSettings");
	        }
	        catch (Exception exception)
	        {
                Debug.WriteLine(exception.Message);
	            MessageBox.Show(exception.Message, " colour Settings Error");
	        }

	        var rgb = data.Colour.Split(',');
	        //ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
		
	        ThemeManagerHelper.CreateAppStyleBy(new Color { R = Convert.ToByte(rgb[0]), G = Convert.ToByte(rgb[1]), B = Convert.ToByte(rgb[2]) }, true);
	    }
		private async Task GetSettings()
		{
			_positions = new List<Position>();

			try
			{
				var electionData = ElectionConfigurationService.ConfigureElection();
			await	BlobCache.UserAccount.InsertObject("ElectionSettings", electionData);
				_positions = ElectionConfigurationService.GetAllPositions();

			await	BlobCache.UserAccount.InsertObject("ElectionPositions", _positions);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, " Election Settings Error");
			}
		}
	}
}
