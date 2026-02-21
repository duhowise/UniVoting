using System;
using System.Windows;
using Wpf.Ui.Appearance;
using UniVoting.Services;

namespace UniVoting.Client
{
    public partial class App : Application
    {
        private static readonly ILogger _logger = new SystemEventLoggerService();

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exp) _logger.Log(exp);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                MainWindow = new ClientsLoginWindow();
                MainWindow.Show();
                base.OnStartup(e);
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
            }
        }
    }
}
