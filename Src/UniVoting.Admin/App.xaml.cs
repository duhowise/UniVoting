using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using UniVoting.Admin.Administrators;
using UniVoting.Admin.StatUp;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin
{
    public partial class App : Application
    {
        private static readonly ILogger _logger = new SystemEventLoggerService();

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                try
                {
                    var bootstrapper = new BootStrapper();
                    bootstrapper.BootStrap();
                    desktop.MainWindow = new AdminLoginWindow();
                }
                catch (System.Exception exception)
                {
                    _logger.Log(exception);
                }
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
