using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
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
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("VotingSystem")
                ?? throw new InvalidOperationException("Connection string 'VotingSystem' not found in appsettings.json.");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddUniVotingServices(connectionString);
            serviceCollection.AddTransient<ClientsLoginWindow>();
            serviceCollection.AddTransient<MainWindow>();
            serviceCollection.AddTransient<ClientVotingPage>();
            serviceCollection.AddTransient<ClientVoteCompletedPage>();
            serviceCollection.AddTransient<CandidateControl>();
            serviceCollection.AddTransient<YesOrNoCandidateControl>();
            serviceCollection.AddTransient<ConfirmDialogControl>();
            serviceCollection.AddTransient<SkipVoteDialogControl>();
            Services = serviceCollection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var logger = Services.GetRequiredService<ILogger>();
                AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    if (e.ExceptionObject is Exception exp) logger.Log(exp);
                };
                desktop.MainWindow = Services.GetRequiredService<ClientsLoginWindow>();
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
