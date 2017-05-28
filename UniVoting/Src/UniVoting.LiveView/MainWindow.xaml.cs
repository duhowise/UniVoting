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
           
        }
	
        private async void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                _positions = await LiveViewService.Positions();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
            foreach (var position in _positions)
            {
                CastedVotesHolder.Children.Add(new TileControlLarge(position.PositionName));
                SkippedVotesHolder.Children.Add(new TileControlSmall(position.PositionName));
            }
        }
    }
}
