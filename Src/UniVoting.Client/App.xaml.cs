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
            serviceCollection.AddTransient<ClientsLoginWindow>();
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
