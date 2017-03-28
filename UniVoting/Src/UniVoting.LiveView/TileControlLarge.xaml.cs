using System;
using System.Collections.Generic;
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

namespace UniVoting.LiveView
{
    /// <summary>
    /// Interaction logic for TileControlLarge.xaml
    /// </summary>
    public partial class TileControlLarge : UserControl
    {
        public TileControlLarge()
        {
            InitializeComponent();
        }

        public TileControlLarge(String position, String votes)
        {
            InitializeComponent();
            this.Tile.Title = position;
            this.VoteCount.Content = votes;
        }
    }
}
