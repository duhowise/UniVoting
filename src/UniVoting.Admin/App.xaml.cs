using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using UniVoting.Admin.Administrators;
using UniVoting.Data.Implementations;
using UniVoting.Data.Interfaces;
using UniVoting.Services;

namespace UniVoting.Admin
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private ServiceProvider _serviceProvider;

		public App()
		{
			var services = new ServiceCollection();
			ConfigureServices(services);
			_serviceProvider = services.BuildServiceProvider();
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
			services.AddDbContext<UniVoting.Data.VotingDbContext>(options =>
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

			// Register repositories
			services.AddScoped<CandidateRepository>();
			services.AddScoped<ComissionerRepository>();
			services.AddScoped<VoterRepository>();
			services.AddScoped<PositionRepository>();

			// Register core service that depends on repositories
			services.AddScoped<IService, ElectionService>();

			// Register business services that depend on IService
			services.AddScoped<VotingService>();
			services.AddScoped<LiveViewService>();
			services.AddScoped<ElectionConfigurationService>();

			// Register Windows/Views for DI
			services.AddTransient<AdminLoginWindow>();
			services.AddTransient<AdminMenuPage>();
			services.AddTransient<AdminAddVotersPage>();
			services.AddTransient<AdminSetUpCandidatesPage>();
			services.AddTransient<AdminSetUpElectionPage>();
			services.AddTransient<AdminSetUpPositionPage>();
			services.AddTransient<AdminCreateAccountPage>();
			services.AddTransient<AdminDispensePasswordWindow>();
			services.AddTransient<EcChairmanLoginWindow>();
			services.AddTransient<PresidentLoginWindow>();
			services.AddTransient<ReportViewerWindow>();
			services.AddTransient<MainWindow>();
		}

        protected override void OnStartup(StartupEventArgs e)
		{
			// Initialize WPF UI theme system
			ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
			ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
			ThemeManagerHelper.CreateAppStyleBy(Colors.Indigo, true);

			// Use DI to get the main window
			var mainWindow = _serviceProvider.GetRequiredService<AdminLoginWindow>();
			mainWindow.Show();
			
			base.OnStartup(e);
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
	}
}
