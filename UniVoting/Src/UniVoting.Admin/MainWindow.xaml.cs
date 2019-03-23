using MahApps.Metro.Controls;
using UniVoting.Admin.Administrators;
using UniVoting.Core;

namespace UniVoting.Admin
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly Commissioner _commissioner;

        public MainWindow(Commissioner commissioner)
        {
            _commissioner = commissioner;
            InitializeComponent();


            PageHolder.Content = new AdminMenuPage(_commissioner);
        }

      
    }
}
