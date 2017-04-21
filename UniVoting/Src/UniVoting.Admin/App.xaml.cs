using System;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro;
using UniVoting.LiveView;
using UniVoting.WPF.Administrators;

namespace UniVoting.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            // add custom accent and theme resource dictionaries
            ThemeManager.AddAccent("CustomAccent1", new Uri("pack://application:,,,/UniVoting.WPF;component/CustomAccents/CustomAccent.xaml"));

            // create custom accents
            ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
            ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
            ThemeManagerHelper.CreateAppStyleBy(Colors.Indigo, true);
            MainWindow=new MainWindow();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
