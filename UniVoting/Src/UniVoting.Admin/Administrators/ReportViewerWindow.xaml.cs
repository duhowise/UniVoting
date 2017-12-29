using Telerik.ReportViewer.Wpf;

namespace UniVoting.WPF.Administrators
{
    using System.Windows;

    public partial class ReportViewerWindow : Window
    {
        public ReportViewerWindow()
        {
            InitializeComponent();
            WindowState=WindowState.Maximized;
            this.ReportViewer1.ZoomMode=ZoomMode.PageWidth;
        }
    }
}