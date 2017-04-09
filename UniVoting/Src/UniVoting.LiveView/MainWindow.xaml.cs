using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using UniVoting.Services;
using Position = UniVoting.Model.Position;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        IEnumerable<Position> _positions;

        public MainWindow()
        {
            InitializeComponent();

            _positions=new List<Position>();
            Loaded += MainWindow_Loaded;
            try
            {
                _positions = ElectionConfigurationService.GetPositionsSlim();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error");
            }

           
           
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var position in _positions)
            {
                CastedVotesHolder.Children.Add(new TileControlLarge(position.PositionName));
                SkippedVotesHolder.Children.Add(new TileControlSmall(position.PositionName));
            }
        }
    }
}
