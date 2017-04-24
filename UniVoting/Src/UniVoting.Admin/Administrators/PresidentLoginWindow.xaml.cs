using System.Windows;
using MahApps.Metro.Controls;

namespace UniVoting.WPF.Administrators
{
	/// <summary>
	/// Interaction logic for PresidentLoginWindow.xaml
	/// </summary>
	public partial class PresidentLoginWindow : MetroWindow
	{
		public PresidentLoginWindow()
		{
			InitializeComponent();
			WindowState=WindowState.Maximized;
			BtnLogin.Click += BtnLogin_Click;
		}

		private void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			new EcChairmanLoginWindow().Show();
			Close();

		}
	}
}
