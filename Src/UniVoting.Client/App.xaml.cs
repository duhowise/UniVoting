using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        public override void Initialize() => AvaloniaXamlLoader.Load(this);

        public override void OnFrameworkInitializationCompleted()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddUniVotingServices();
            Services = serviceCollection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var electionService = Services.GetRequiredService<IElectionConfigurationService>();
                var votingService = Services.GetRequiredService<IVotingService>();
                var logger = Services.GetRequiredService<ILogger>();
                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    if (e.ExceptionObject is Exception exp) logger.Log(exp);
                };
                desktop.MainWindow = new ClientsLoginWindow(electionService, votingService, logger);
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
