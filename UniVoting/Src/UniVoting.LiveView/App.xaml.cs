using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // add custom accent and theme resource dictionaries
            ThemeManager.AddAccent("CustomAccent1", new Uri("pack://application:,,,/UniVoting.LiveView;component/CustomAccents/CustomAccent.xaml"));
           
            // create custom accents
            ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
            ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
            ThemeManagerHelper.CreateAppStyleBy(Colors.Indigo, true);

            base.OnStartup(e);
        }
    }
}
