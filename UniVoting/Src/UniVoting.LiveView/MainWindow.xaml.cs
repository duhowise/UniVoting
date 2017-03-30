using MahApps.Metro.Controls;

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

            for (int i = 0; i < 10; i++)
            {
                CastedVotesHolder.Children.Add(new TileControlLarge("Public Relations Officer"));
            }

            for (int i = 0; i < 20; i++)
            {
                SkippedVotesHolder.Children.Add(new TileControlSmall("Secretary"));
            }
        }
    }
}
