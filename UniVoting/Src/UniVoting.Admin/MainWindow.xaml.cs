using Autofac;
using MahApps.Metro.Controls;
using UniVoting.Admin.Administrators;
using UniVoting.Admin.Startup;
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
      private  IContainer container;

        public MainWindow(Commissioner commissioner)
        {
            _commissioner = commissioner;
            InitializeComponent();
            container = new BootStrapper().BootStrap();
            PageHolder.Content = new AdminMenuPage(_commissioner);
        }

      
    }
}
