using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.LiveView
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
                var liveViewService = Services.GetRequiredService<ILiveViewService>();
                var logger = Services.GetRequiredService<ILogger>();
                desktop.MainWindow = new MainWindow(liveViewService, logger);
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
