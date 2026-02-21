using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Admin.Administrators;
using UniVoting.Services;

namespace UniVoting.Admin
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("VotingSystem")
                ?? throw new InvalidOperationException("Connection string 'VotingSystem' not found in appsettings.json.");

            if (connectionString.Contains("CHANGE_ME", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Please update the connection string in appsettings.json before running the application.");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddUniVotingServices(connectionString);
            serviceCollection.AddTransient<AdminLoginWindow>();
            serviceCollection.AddTransient<MainWindow>();
            serviceCollection.AddTransient<Administrators.AdminMenuPage>();
            serviceCollection.AddTransient<Administrators.AdminDispensePasswordWindow>();
            serviceCollection.AddTransient<Administrators.PresidentLoginWindow>();
            serviceCollection.AddTransient<Administrators.EcChairmanLoginWindow>();
            serviceCollection.AddTransient<Administrators.ReportViewerWindow>();
            serviceCollection.AddTransient<Administrators.AdminSetUpPositionPage>();
            serviceCollection.AddTransient<Administrators.AdminSetUpElectionPage>();
            serviceCollection.AddTransient<Administrators.AdminCreateAccountPage>();
            serviceCollection.AddTransient<Administrators.AdminSetUpCandidatesPage>();
            serviceCollection.AddTransient<Administrators.AdminAddVotersPage>();
            serviceCollection.AddTransient<Administrators.AddPositionDialogControl>();
            serviceCollection.AddTransient<Administrators.PositionControl>();
            serviceCollection.AddTransient<Administrators.ResetPasswordWindow>();
            serviceCollection.AddSingleton<IAdminSessionService, AdminSessionService>();
            Services = serviceCollection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = Services.GetRequiredService<AdminLoginWindow>();
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
