using System.ComponentModel;
using System.Windows;
using UniVoting.Admin.Administrators;
using UniVoting.Model;

namespace UniVoting.Admin
{
    public partial class MainWindow : Window
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
