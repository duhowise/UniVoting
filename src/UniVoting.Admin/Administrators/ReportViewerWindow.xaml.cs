using Microsoft.Win32;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace UniVoting.Admin.Administrators
{
    public partial class ReportViewerWindow : Window
    {
        public ReportViewerWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            statusText.Content = "Report viewer initialized";
        }
        
        public void LoadReportWithData(DataTable dataTable, string reportTitle = "Election Report")
        {
            try
            {
                // Create a simple data view instead of full reporting
                CreateSimpleDataGrid(dataTable, reportTitle);
                statusText.Content = $"Report loaded with {dataTable.Rows.Count} records";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}", "Load Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        public void LoadElectionResults(DataTable electionData, string electionTitle = "Election Results")
        {
            try
            {
                CreateSimpleDataGrid(electionData, electionTitle);
                statusText.Content = $"Election report loaded: {electionData.Rows.Count} results";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading election results: {ex.Message}", "Load Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void CreateSimpleDataGrid(DataTable dataTable, string title)
        {
            // Clear existing content
            previewHost.Child = null;
            
            // Create a WinForms Panel to host our report display
            var panel = new System.Windows.Forms.Panel();
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.BackColor = System.Drawing.Color.White;
            
            // Create title label
            var titleLabel = new System.Windows.Forms.Label();
            titleLabel.Text = title.ToUpper();
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);
            titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            titleLabel.Height = 60;
            titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            titleLabel.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            panel.Controls.Add(titleLabel);
            
            // Create DataGridView for the data
            var dataGrid = new System.Windows.Forms.DataGridView();
            dataGrid.DataSource = dataTable;
            dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGrid.ReadOnly = true;
            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            
            // Style the grid
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 230, 241);
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dataGrid.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9);
            
            panel.Controls.Add(dataGrid);
            previewHost.Child = panel;
        }
        
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Simple print functionality
                if (previewHost.Child is System.Windows.Forms.Panel panel)
                {
                    MessageBox.Show("Print functionality will be implemented with FastReport templates.", 
                        "Print", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                statusText.Content = "Print functionality ready";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing: {ex.Message}", "Print Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void btnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    DefaultExt = "pdf",
                    FileName = $"Election_Report_{DateTime.Now:yyyyMMdd}.pdf"
                };
                
                if (saveDialog.ShowDialog() == true)
                {
                    // This would be implemented with FastReport PDF export
                    MessageBox.Show("PDF export functionality will be implemented with FastReport templates.", 
                        "Export PDF", MessageBoxButton.OK, MessageBoxImage.Information);
                    statusText.Content = $"PDF export ready: {saveDialog.FileName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Export Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void btnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveDialog = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    DefaultExt = "xlsx",
                    FileName = $"Election_Report_{DateTime.Now:yyyyMMdd}.xlsx"
                };
                
                if (saveDialog.ShowDialog() == true)
                {
                    // This would be implemented with FastReport Excel export
                    MessageBox.Show("Excel export functionality will be implemented with FastReport templates.", 
                        "Export Excel", MessageBoxButton.OK, MessageBoxImage.Information);
                    statusText.Content = $"Excel export ready: {saveDialog.FileName}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Export Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void btnDesigner_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Report designer functionality will be implemented with FastReport templates.", 
                    "Designer", MessageBoxButton.OK, MessageBoxImage.Information);
                statusText.Content = "Report designer ready";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening designer: {ex.Message}", "Designer Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void cmbZoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedItem = cmbZoom.SelectedItem as ComboBoxItem;
                if (selectedItem == null) return;
                
                var zoomText = selectedItem.Content.ToString();
                statusText.Content = $"Zoom set to {zoomText}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting zoom: {ex.Message}", "Zoom Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}