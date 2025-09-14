using System.Windows;
using Microsoft.Reporting.WinForms;

namespace UniVoting.Admin.Administrators
{
    public partial class ReportViewerWindow : Window
    {
        public ReportViewerWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            this.ReportViewer1.ZoomMode = ZoomMode.PageWidth;
        }
    }
}