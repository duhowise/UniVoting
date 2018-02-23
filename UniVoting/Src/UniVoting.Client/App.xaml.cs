using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Akavache;
using UniVoting.Model;
using UniVoting.Services;


namespace UniVoting.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
	    private Setting _electionData;

		private IEnumerable<Position> _positions;
		public App()
		{
            BlobCache.ApplicationName = $"VotingApplication";
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
		    if (e.ExceptionObject is Exception exp) MessageBox.Show(exp.Message);
		}
	
		protected override async void OnStartup(StartupEventArgs e)
		{
		    await	BlobCache.UserAccount.InvalidateAll();

		    await GetSettings();
		    await SetTheme();
		    MainWindow = new ClientsLoginWindow();
		    MainWindow.Show();
		    base.OnStartup(e);
		}

	    public static async Task SetTheme()
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
                //ElectionSettings
		        _electionData = await BlobCache.UserAccount.GetObject<Setting>("ElectionSettings")
		            .Catch(Observable.Return(_electionData = ElectionConfigurationService.ConfigureElection()));
		      await	BlobCache.UserAccount.InsertObject("ElectionSettings", _electionData);
		        //ElectionPositions
		        _positions = await BlobCache.UserAccount.GetObject<IEnumerable<Position>>("ElectionPositions")
		            .Catch(Observable.Return( _positions = ElectionConfigurationService.GetAllPositions()));
		        await	BlobCache.UserAccount.InsertObject("ElectionPositions", _positions);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, " Election Settings Error");
			}
		}
	}
}
