using System;
using Avalonia.Controls;
using UniVoting.Admin.Administrators;
using UniVoting.Model;

namespace UniVoting.Admin
{
    public partial class MainWindow : Window
    {
        public static Action<object?>? Navigate;

        public MainWindow(Comissioner comissioner)
        {
            InitializeComponent();
            Navigate = (content) => PageHolder.Content = content;
            PageHolder.Content = new AdminMenuPage(comissioner);
        }

        protected override void OnClosing(Avalonia.Controls.WindowClosingEventArgs e)
        {
            new AdminLoginWindow().Show();
            base.OnClosing(e);
        }
    }
}
