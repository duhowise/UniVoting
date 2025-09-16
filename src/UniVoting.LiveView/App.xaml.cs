using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using UniVoting.Services;
using Wpf.Ui.Appearance;

namespace UniVoting.LiveView
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
            services.AddDbContext<Data.VotingDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Register business services that depend on VotingDbContext
            services.AddScoped<VotingService>();
            services.AddScoped<LiveViewService>();
            services.AddScoped<ElectionConfigurationService>();

            // Register Windows/Views for DI
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Initialize WPF UI theme system
            ApplicationThemeManager.Apply(ApplicationTheme.Dark);
            
            // Watch for system theme changes
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            if (mainWindow != null)
            {
                SystemThemeWatcher.Watch(mainWindow);
            }
            
            // Create custom accent colors using the WPF UI compatible ThemeManagerHelper
            ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
            ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
            ThemeManagerHelper.CreateAppStyleBy(Colors.Indigo, true);

            // Show main window
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
