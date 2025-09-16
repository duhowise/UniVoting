using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
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
	    private static ILogger<App> _logger;
	    private ICacheService _cacheService;
	    private ServiceProvider _serviceProvider;

        private IEnumerable<Position> _positions;
        
		public App()
		{
		    try
		    {
		        var services = new ServiceCollection();
		        ConfigureServices(services);
		        _serviceProvider = services.BuildServiceProvider();
		        
		        _cacheService = _serviceProvider.GetRequiredService<ICacheService>();
		        _logger = _serviceProvider.GetRequiredService<ILogger<App>>();
		        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            }
		    catch (Exception e)
		    {
		        _logger?.LogError(e, "Failed to initialize application");
		    }
		}

        private void ConfigureServices(ServiceCollection services)
        {
            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Register caching services
            services.AddMemoryCache();
            services.AddScoped<ICacheService, MemoryCacheService>();

            // Register Microsoft Extensions Logging
            services.AddLogging(builder => builder.AddConsole().AddDebug());

            // Register VotingDbContext with MySQL connection string
            var connectionString = configuration.GetConnectionString("VotingSystem");
            services.AddDbContext<Data.VotingDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Register business services that depend on VotingDbContext
            services.AddScoped<VotingService>();
            services.AddScoped<LiveViewService>();
            services.AddScoped<ElectionConfigurationService>();

            // Register Windows/Views for DI
            services.AddTransient<ClientsLoginWindow>();
            services.AddTransient<MainWindow>();
            services.AddTransient<ClientVoteCompletedPage>();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
		    if (!(e.ExceptionObject is Exception exp)) return;
		    MessageBox.Show(exp.Message, " colour Settings Error");
		    _logger?.LogError(exp, "Unhandled exception occurred");

		}

        protected override async void OnStartup(StartupEventArgs e)
		{
		    try
		    {
		        await _cacheService.InvalidateAllAsync();

		        await GetSettings();
		        await SetTheme();
		        
		        var mainWindow = _serviceProvider.GetRequiredService<ClientsLoginWindow>();
		        mainWindow.Show();
		        
		        base.OnStartup(e);
            }
		    catch (Exception exception)
		    {
		        _logger?.LogError(exception, "Failed to start application");
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider?.Dispose();
            base.OnExit(e);
        }

        public static T GetService<T>() where T : class
        {
            return ((App)Current)?._serviceProvider?.GetService<T>();
        }

	    public async Task SetTheme()
	    {
	        var data = new Setting();

	        try
	        {
	            //get color from local cache
	            data = await _cacheService.GetObjectAsync<Setting>("ElectionSettings");
	            if (data == null)
	            {
	                // Load default settings if cache is empty
	                var configService = _serviceProvider.GetRequiredService<ElectionConfigurationService>();
	                data = configService.ConfigureElection();
	            }
	        }
	        catch (Exception exception)
	        {
	            MessageBox.Show(exception.Message, " colour Settings Error");
	        }

	        if (!string.IsNullOrEmpty(data?.Colour))
	        {
	            var rgb = data.Colour.Split(',');
	            //ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
		
	            ThemeManagerHelper.CreateAppStyleBy(new Color { R = Convert.ToByte(rgb[0]), G = Convert.ToByte(rgb[1]), B = Convert.ToByte(rgb[2]) }, true);
	        }
	    }
		private async Task GetSettings()
		{
			_positions = new List<Position>();
            try
		    {
                //ElectionSettings
		        _electionData = await _cacheService.GetObjectAsync<Setting>("ElectionSettings");
		        if (_electionData == null)
		        {
		            var configService = _serviceProvider.GetRequiredService<ElectionConfigurationService>();
		            _electionData = configService.ConfigureElection();
		            await _cacheService.InsertObjectAsync("ElectionSettings", _electionData);
		        }
		        
		        //ElectionPositions
		        _positions = await _cacheService.GetObjectAsync<IEnumerable<Position>>("ElectionPositions");
		        if (_positions == null)
		        {
		            var configService = _serviceProvider.GetRequiredService<ElectionConfigurationService>();
		            _positions = configService.GetAllPositions();
		            await _cacheService.InsertObjectAsync("ElectionPositions", _positions);
		        }
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, " Election Settings Error");
			}
		}
	}
}
