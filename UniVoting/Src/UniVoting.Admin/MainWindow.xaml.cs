using MahApps.Metro.Controls;
using System.ComponentModel;
using UniVoting.Admin.Administrators;
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
			new AdminLoginWindow().Show();
			base.OnClosing(e);
		}


    }
}
