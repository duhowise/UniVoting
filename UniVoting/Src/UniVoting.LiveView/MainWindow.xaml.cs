using MahApps.Metro.Controls;
using UniVoting.Services;

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var positions = ElectionConfigurationService.GetPositionsSlim();
            
            foreach (var position in positions)
            {
                CastedVotesHolder.Children.Add(new TileControlLarge(position.PositionName));
                SkippedVotesHolder.Children.Add(new TileControlSmall(position.PositionName));
            }
           
        }
    }
}
