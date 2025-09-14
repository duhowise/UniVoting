using System;
using System.Data;
using System.Windows.Forms;

namespace VoteReport
{
    /// <summary>
    /// Summary description for VoteCountReport - Simplified version compatible with .NET 8
    /// </summary>
    public partial class VoteCountReport : UserControl
    {
        private DataGridView dataGridView;
        private Label titleLabel;
        private Label positionLabel;
        
        public VoteCountReport()
        {
            InitializeComponent();
            SetupControls();
        }
        
        private void SetupControls()
        {
            // Setup layout
            this.Dock = DockStyle.Fill;
            this.BackColor = System.Drawing.Color.White;
            
            // Create title label
            titleLabel = new Label();
            titleLabel.Text = "UniVoting Election System";
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold);
            titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            titleLabel.Height = 50;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.Controls.Add(titleLabel);
            
            // Create position label
            positionLabel = new Label();
            positionLabel.Text = "Vote Count Report";
            positionLabel.Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Regular);
            positionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            positionLabel.Height = 35;
            positionLabel.Dock = DockStyle.Top;
            positionLabel.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);
            this.Controls.Add(positionLabel);
            
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
        
        public void LoadReport(string reportPath, object dataSource = null)
        {
            try
            {
                if (dataSource is DataTable dataTable)
                {
                    LoadVoteCountReport(dataTable);
                }
                else if (dataSource is DataSet dataSet && dataSet.Tables.Count > 0)
                {
                    LoadVoteCountReport(dataSet.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report: {ex.Message}", "Report Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void LoadVoteCountReport(DataTable voteData, string positionFilter = "")
        {
            try
            {
                if (voteData == null)
                {
                    MessageBox.Show("No data provided for report", "Data Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Update position label if filter is provided
                if (!string.IsNullOrEmpty(positionFilter))
                {
                    positionLabel.Text = $"Position: {positionFilter}";
                }
                
                // Set the data source
                dataGridView.DataSource = voteData;
                
                // Format column headers
                FormatDataGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vote count report: {ex.Message}", "Report Error", 
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
            // Convert camelCase or PascalCase to readable format
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
                // Basic print functionality
                MessageBox.Show("Print functionality will be enhanced with FastReport templates in future updates.", 
                    "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing report: {ex.Message}", "Print Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void ExportReport(string format, string filePath)
        {
            try
            {
                // Basic export functionality placeholder
                MessageBox.Show($"Export to {format.ToUpper()} will be implemented with FastReport templates.", 
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", 
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
        
        public void RefreshReport()
        {
            try
            {
                if (dataGridView.DataSource != null)
                {
                    dataGridView.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing report: {ex.Message}", "Refresh Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}