using System.Windows;
using MahApps.Metro.Controls;

namespace UniVoting.WPF.Administrators
{
	/// <summary>
	/// Interaction logic for ECChairmanLoginWindow.xaml
	/// </summary>
	public partial class EcChairmanLoginWindow : MetroWindow
	{
		public EcChairmanLoginWindow()
		{
			InitializeComponent();
			WindowState = WindowState.Maximized;
			BtnLogin.Click += BtnLogin_Click;
		}

		private void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			new ReportViewerWindow().Show();
			Close();
		}
	}
}
