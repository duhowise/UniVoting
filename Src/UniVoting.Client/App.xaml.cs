using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Client
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
                    AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                    {
                        if (e.ExceptionObject is Exception exp) _logger.Log(exp);
                    };
                    desktop.MainWindow = new ClientsLoginWindow();
                }
                catch (Exception exception)
                {
                    _logger.Log(exception);
                }
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
