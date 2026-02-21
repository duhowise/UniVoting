using System;
using System.Windows;
using Wpf.Ui.Appearance;
using UniVoting.Admin.Administrators;
using UniVoting.Admin.StatUp;
using UniVoting.Model;
using UniVoting.Services;

namespace UniVoting.Admin
{
    public partial class App : Application
    {
        private static readonly ILogger _logger = new SystemEventLoggerService();

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var bootstrapper = new BootStrapper();
                var container = bootstrapper.BootStrap();
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
                MainWindow = new AdminLoginWindow();
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
