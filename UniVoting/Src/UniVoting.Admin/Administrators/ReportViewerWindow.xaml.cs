using System.Windows;
using Telerik.ReportViewer.Wpf;

namespace UniVoting.Admin.Administrators
{
    public partial class ReportViewerWindow : Window
    {
        public ReportViewerWindow()
        {
            InitializeComponent();
            WindowState=WindowState.Maximized;
            ReportViewer1.ZoomMode=ZoomMode.PageWidth;
        }
    }
}