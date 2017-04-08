using Telerik.ReportViewer.Wpf;

namespace UniVoting.WPF.Administrators
{
    using System;
    using System.Windows;
    using Telerik.Windows.Controls;

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