using System;
using System.Data;
using System.Windows.Forms;

namespace Univoting.Reporting
{
    /// <summary>
    /// Enhanced VoteReport using basic .NET 8 components with future FastReport.OpenSource integration
    /// </summary>
    public partial class VoteReport : UserControl
    {
        private DataGridView dataGridView;
        private Label titleLabel;
        private Panel headerPanel;
        
        public VoteReport()
        {
            InitializeComponent();
            SetupControls();
        }
        
        private void SetupControls()
        {
            // Setup layout
            this.Dock = DockStyle.Fill;
            this.BackColor = System.Drawing.Color.White;
            
            // Create header panel
            headerPanel = new Panel();
            headerPanel.Height = 100;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(headerPanel);
            
            // Create title label
            titleLabel = new Label();
            titleLabel.Text = "UniVoting Election Report";
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            titleLabel.Dock = DockStyle.Fill;
            headerPanel.Controls.Add(titleLabel);
            
            // Create data grid
            dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            
            // Style the grid
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 230, 241);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            dataGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9);
            dataGridView.ColumnHeadersHeight = 35;
            dataGridView.RowTemplate.Height = 30;
            
            this.Controls.Add(dataGridView);
        }
        
        public void LoadElectionReport(DataTable electionData, string electionTitle = "Election Results")
        {
            try
            {
                titleLabel.Text = electionTitle.ToUpper();
                dataGridView.DataSource = electionData;
                FormatDataGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading election report: {ex.Message}", "Report Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void LoadVoteAnalysisReport(DataTable voteData, DataTable summaryData = null)
        {
            try
            {
                titleLabel.Text = "VOTE ANALYSIS REPORT";
                
                // For now, show the main vote data
                // In future, this could be enhanced to show multiple tables
                dataGridView.DataSource = voteData;
                FormatDataGridColumns();
                
                if (summaryData != null)
                {
                    // Could add summary information to header panel
                    var summaryLabel = new Label();
                    summaryLabel.Text = $"Analysis of {voteData?.Rows.Count ?? 0} vote records";
                    summaryLabel.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Italic);
                    summaryLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                    summaryLabel.Height = 25;
                    summaryLabel.Dock = DockStyle.Bottom;
                    headerPanel.Controls.Add(summaryLabel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vote analysis report: {ex.Message}", "Report Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void FormatDataGridColumns()
        {
            if (dataGridView.Columns.Count == 0) return;
            
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                // Format column header
                column.HeaderText = FormatColumnName(column.HeaderText);
                
                // Set column alignment based on data type
                if (IsNumericColumn(column))
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                
                // Style column header
                column.HeaderCell.Style.BackColor = System.Drawing.Color.FromArgb(220, 230, 241);
                column.HeaderCell.Style.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
                column.HeaderCell.Style.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            }
        }
        
        private string FormatColumnName(string columnName)
        {
            var result = System.Text.RegularExpressions.Regex.Replace(columnName, 
                "(\\B[A-Z])", " $1");
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());
        }
        
        private bool IsNumericColumn(DataGridViewColumn column)
        {
            if (column.ValueType == null) return false;
            
            return column.ValueType == typeof(int) || 
                   column.ValueType == typeof(long) || 
                   column.ValueType == typeof(decimal) || 
                   column.ValueType == typeof(double) || 
                   column.ValueType == typeof(float);
        }
        
        public void PrintReport()
        {
            try
            {
                MessageBox.Show("Enhanced print functionality will be available with FastReport templates.", 
                    "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing report: {ex.Message}", "Print Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void ExportToPdf(string filePath)
        {
            try
            {
                MessageBox.Show("PDF export will be available with FastReport templates.", 
                    "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void ExportToExcel(string filePath)
        {
            try
            {
                MessageBox.Show("Excel export will be available with FastReport templates.", 
                    "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void ShowDesigner()
        {
            try
            {
                MessageBox.Show("Report designer will be available with FastReport templates.", 
                    "Designer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening report designer: {ex.Message}", "Designer Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}