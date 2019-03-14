using System.ComponentModel;
using Autofac;
using MahApps.Metro.Controls;
using Prism.Events;
using Univoting.Core;
using UniVoting.Admin.Administrators;
using UniVoting.Admin.Events;
using UniVoting.Admin.Startup;

namespace UniVoting.Admin
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
        private IEventAggregator _eventAggregator;
        //private readonly IEventAggregator _eventAggregator;

        public MainWindow(IEventAggregator eventAggregator)
		{
            _eventAggregator = eventAggregator;
            //_eventAggregator = eventAggregator;
            InitializeComponent();
            
            Loaded += MainWindow_Loaded;


        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _eventAggregator.GetEvent<LoginValidEvent>().Subscribe(ManageLoginEvent);
          
        }

        private void ManageLoginEvent(Commissioner obj)
        {
            var container = new BootStrapper().BootStrap();
            var adminMenuPage = container.Resolve<AdminMenuPage>();
            PageHolder.Content = adminMenuPage;
            throw new System.NotImplementedException();
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            var container = new BootStrapper().BootStrap();
            base.OnClosing(e);

            var window = container.Resolve<AdminLoginWindow>();

			window.Show();
		}


    }
}
