using System.ComponentModel;
using Autofac;
using MahApps.Metro.Controls;
using UniVoting.Admin.Administrators;
using UniVoting.Admin.Startup;
using UniVoting.Core;

namespace UniVoting.Admin
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{

        public MainWindow(Commissioner commissioner)
		{
			InitializeComponent();
           
			
		   PageHolder.Content = new AdminMenuPage(commissioner);

		}
        
        protected override void OnClosing(CancelEventArgs e)
        {
            var container = new BootStrapper().BootStrap();

            var window = container.Resolve<AdminLoginWindow>();

			window.Show();
			base.OnClosing(e);
		}


    }
}
