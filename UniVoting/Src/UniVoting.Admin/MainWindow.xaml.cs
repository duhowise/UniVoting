using System.ComponentModel;
using MahApps.Metro.Controls;
using UniVoting.Model;
using UniVoting.WPF.Administrators;

namespace UniVoting.WPF
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
