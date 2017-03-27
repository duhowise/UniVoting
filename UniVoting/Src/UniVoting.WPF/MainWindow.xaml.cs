using MahApps.Metro.Controls;
using UniVoting.WPF.Administrators;

namespace UniVoting.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
           PageHolder.Content = new AdminMenuPage();

        }
    }
}
