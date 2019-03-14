using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using Akavache;
using Autofac;
using Univoting.Core;
using Univoting.Services;
using UniVoting.Client.Startup;


namespace UniVoting.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
		private ElectionConfiguration _electionData;
	    //private static readonly ILogger _logger = new SystemEventLoggerService();

        private IEnumerable<Position> _positions;
		public App()
		{

			//_electionConfigurationService = electionConfigurationService;
			try
		    {
		        BlobCache.ApplicationName = $"VotingApplication";
		        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            }
		    catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\n {exception.InnerException}");

            }
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
		    //if (e.ExceptionObject is Exception exp) _logger.Log(exp);
		}
	
		protected override async void OnStartup(StartupEventArgs e)
		{
		    try
		    {
                var container = new BootStrapper().BootStrap();
                var electionConfigurationService = container.Resolve<IElectionConfigurationService>();
                await BlobCache.UserAccount.InvalidateAll();

		        await GetSettings(electionConfigurationService);
		        await SetTheme();

		      

		        var window = container.Resolve<ClientsLoginWindow>();
                MainWindow = window;
                MainWindow?.Show();
                base.OnStartup(e);
            }
		    catch (Exception exception)
		    {
                MessageBox.Show($"{exception.Message}\n {exception.InnerException}");


            }

        }

	    public static async Task SetTheme()
	    {
	        var data = new ElectionConfiguration();

	        try
	        {
	            //get color from  local cache
	            data = await BlobCache.UserAccount.GetObject<ElectionConfiguration>("ElectionSettings");
	        }
	        catch (Exception exception)
	        {
	            MessageBox.Show(exception.Message, " colour Settings Error");
	        }

	        var rgb = data.Colour.Split(',');
	        //ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
		
	        //ThemeManagerHelper.CreateAppStyleBy(new Color { R = Convert.ToByte(rgb[0]), G = Convert.ToByte(rgb[1]), B = Convert.ToByte(rgb[2]) }, true);
	    }
		private async Task GetSettings(IElectionConfigurationService electionConfigurationService)
		{
			_positions = new List<Position>();
		    _electionData=new ElectionConfiguration();
            try
		    {
                //ElectionSettings
		        _electionData = await BlobCache.UserAccount.GetObject<ElectionConfiguration>("ElectionSettings")
		            .Catch(Observable.Return(_electionData =await electionConfigurationService.ConfigureElection()));

		        if (_electionData!=null) 
		        {
		      await	BlobCache.UserAccount.InsertObject("ElectionSettings", _electionData);

                }
                //ElectionPositions
                _positions = await BlobCache.UserAccount.GetObject<IEnumerable<Position>>("ElectionPositions")
		            .Catch(Observable.Return( _positions =await electionConfigurationService.GetAllPositionsAsync()));
		        var enumerable = _positions.ToList();
		        if (enumerable.Any())
		        {
		        await	BlobCache.UserAccount.InsertObject("ElectionPositions", enumerable);

                }

            }
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, " Election Settings Error");
			}
		}
	}
}
