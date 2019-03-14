using System.ComponentModel;
using Autofac;
using MahApps.Metro.Controls;
using UniVoting.Admin.Administrators;
using UniVoting.Admin.Startup;
using UniVoting.Model;

namespace UniVoting.Admin
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{

        public MainWindow(Comissioner comissioner)
		{
			InitializeComponent();
           
			
		   PageHolder.Content = new AdminMenuPage(comissioner);

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
