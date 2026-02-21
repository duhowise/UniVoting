using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
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
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddUniVotingServices();
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
            Services = serviceCollection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = Services.GetRequiredService<AdminLoginWindow>();
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
