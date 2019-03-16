using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Akavache;
using Autofac;
using MahApps.Metro.Controls;
using Univoting.Services;
using UniVoting.Client.Startup;
using UniVoting.Core;
using UniVoting.Services;
using Position = UniVoting.Core.Position;


namespace UniVoting.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
        private static IElectionConfigurationService _electionConfigurationService;
		private ElectionConfiguration _electionData;
        static IContainer _container;

        private IEnumerable<Position> _positions;
        private static MetroWindow _window;

        public App()
        {

            try
            {
                _container = new BootStrapper().BootStrap();
                 _window   = _container.Resolve<ClientsLoginWindow>();
                _electionConfigurationService = _container.Resolve<IElectionConfigurationService>();
                BlobCache.ApplicationName = $"VotingApplication";
		        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            }
		    catch (Exception e)
            {
                _container.Resolve<IDialogService>().ShowMessageAsync(_window, "Error", e.Message).Wait();


            }
        }

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
            if (e.ExceptionObject is Exception exp) _container.Resolve<IDialogService>().ShowMessageAsync(_window,"Error",exp.Message);
		}
	
		protected override async void OnStartup(StartupEventArgs e)
		{
		    try
		    {
                await BlobCache.UserAccount.InvalidateAll();

                await GetSettings();
		        await SetTheme();
                MainWindow = _window;
		        MainWindow?.Show();
		        base.OnStartup(e);
            }
		    catch (Exception exception)
		    {
                //_logger.Log(exception);
                MessageBox.Show(exception.Message, " Startup");

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
                MessageBox.Show(exception.Message, " colour ElectionConfigurations Error");

            }

	        var rgb = data.Colour.Split(',');
            //ThemeManagerHelper.CreateAppStyleBy(Colors.Red);

            ThemeManagerHelper.CreateAppStyleBy(new Color { R = Convert.ToByte(rgb[0]), G = Convert.ToByte(rgb[1]), B = Convert.ToByte(rgb[2]) }, true);
        }
		private async Task GetSettings()
		{
			_positions = new List<Position>();
		    _electionData=new ElectionConfiguration();
            try
		    {
                //ElectionSettings
		        _electionData = await BlobCache.UserAccount.GetObject<ElectionConfiguration>("ElectionSettings")
		            .Catch(Observable.Return(_electionData =await _electionConfigurationService.ConfigureElectionAsync()));

		        if (_electionData!=null) 
		        {
		      await	BlobCache.UserAccount.InsertObject("ElectionSettings", _electionData);

                }
                //ElectionPositions
                _positions = await BlobCache.UserAccount.GetObject<IEnumerable<Position>>("ElectionPositions")
		            .Catch(Observable.Return( _positions =await _electionConfigurationService.GetAllPositionsAsync()));
		        var enumerable = _positions.ToList();
		        if (enumerable.Any())
		        {
		        await	BlobCache.UserAccount.InsertObject("ElectionPositions", enumerable);

                }

            }
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, " Election ElectionConfigurations Error");
			}
		}
	}
}
