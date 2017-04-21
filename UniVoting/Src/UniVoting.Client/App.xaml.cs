using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Akavache;
using MahApps.Metro;
using UniVoting.Model;
using UniVoting.Services;
using Settings = UniVoting.Client.Properties.Settings;

namespace UniVoting.Client
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IEnumerable<Position> _positions;

        protected override void OnStartup(StartupEventArgs e)
        {
            BlobCache.ApplicationName = "VotingApplication";
            if (Settings.Default.FirstRun)
            {
                _positions = new List<Position>();
                

                try
                {
                    var electionData = ElectionConfigurationService.ConfigureElection();
                    _positions = ElectionConfigurationService.GetAllPositions();
                    BlobCache.LocalMachine.InsertObject("ElectionSettings", electionData);
                    BlobCache.LocalMachine.InsertObject("ElectionPositions", _positions);

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, " ElectionSettings Error");
                }
               
                Settings.Default.FirstRun = false;
                Settings.Default.Save();
            }

            // add custom accent and theme resource dictionaries
            //ThemeManager.AddAccent("CustomAccent1",
            //    new Uri("pack://application:,,,/UniVoting.Client;component/CustomAccents/CustomAccent.xaml"));

            // create custom accents
            ThemeManagerHelper.CreateAppStyleBy(Colors.Red);
            ThemeManagerHelper.CreateAppStyleBy(Colors.GreenYellow);
            ThemeManagerHelper.CreateAppStyleBy(new Color {R = 12, G = 130, B = 144}, true);
            MainWindow = new ClientsLoginWindow();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
