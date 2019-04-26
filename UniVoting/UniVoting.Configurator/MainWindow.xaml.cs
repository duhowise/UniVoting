using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.Win32;

namespace UniVoting.Configurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ConfigLoader.Click += ConfigLoader_Click;
        }

        private void ConfigLoader_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog(){ DefaultExt = ".config" };
            if (openFileDialog.ShowDialog() != true) return;
            try
            {
                LoadedFile.Content += openFileDialog.SafeFileName;
                //var connections = new Poker.ConfigurationPoker(openFileDialog.FileName);
                Import(openFileDialog.FileName);
            }
            catch (Exception exception)
            {
                // ignored
            }
        }


        internal static void Import(string settingsFilePath)
        {
            if (!File.Exists(settingsFilePath))
            {
                throw new FileNotFoundException();
            }

            var appSettings = Properties.Settings.Default;
            try
            {
                var config =
                    ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.PerUserRoamingAndLocal);
                var connectionToRemove = config.ConnectionStrings.ConnectionStrings["VotingSystem"];
               //config.ConnectionStrings.ConnectionStrings.Remove(connectionToRemove);



               

                appSettings.Reload();
            }
            catch (Exception) // Should make this more specific
            {
                // Could not import settings.
                appSettings.Reload(); // from last set saved, not defaults
            }
        }

    }
}
