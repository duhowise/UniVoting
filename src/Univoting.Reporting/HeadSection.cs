namespace Univoting.Reporting
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Enhanced HeadSection component for election report headers
    /// </summary>
    public partial class HeadSection : UserControl
    {
        private Label titleLabel;
        private Label subtitleLabel;
        private Label dateLabel;
        private Panel headerPanel;
        
        public HeadSection()
        {
            InitializeComponent();
            SetupHeaderControls();
        }
        
        private void SetupHeaderControls()
        {
            // Setup layout
            this.Dock = DockStyle.Fill;
            this.BackColor = System.Drawing.Color.White;
            
            // Create main header panel
            headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            headerPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(headerPanel);
            
            // Create title label
            titleLabel = new Label();
            titleLabel.Text = "UniVoting Election System";
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            titleLabel.ForeColor = System.Drawing.Color.FromArgb(51, 51, 51);
            titleLabel.Height = 50;
            titleLabel.Dock = DockStyle.Top;
            headerPanel.Controls.Add(titleLabel);
            
            // Create subtitle label
            subtitleLabel = new Label();
            subtitleLabel.Text = "Official Election Report";
            subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Regular);
            subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
            subtitleLabel.Height = 35;
            subtitleLabel.Dock = DockStyle.Top;
            headerPanel.Controls.Add(subtitleLabel);
            
            // Create date label
            dateLabel = new Label();
            dateLabel.Text = $"Generated: {DateTime.Now:MMMM dd, yyyy 'at' HH:mm}";
            dateLabel.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Italic);
            dateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            dateLabel.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            dateLabel.Height = 25;
            dateLabel.Dock = DockStyle.Bottom;
            headerPanel.Controls.Add(dateLabel);
        }
        
        public void LoadElectionHeader(string electionTitle, string institution = "", DateTime? electionDate = null)
        {
            try
            {
                // Update title with election information
                if (!string.IsNullOrEmpty(institution))
                {
                    titleLabel.Text = institution.ToUpper();
                    subtitleLabel.Text = electionTitle.ToUpper();
                }
                else
                {
                    titleLabel.Text = electionTitle.ToUpper();
                    subtitleLabel.Text = "Official Election Report";
                }
                
                // Update date information
                var dateDisplayText = electionDate?.ToString("MMMM dd, yyyy") ?? DateTime.Now.ToString("MMMM dd, yyyy");
                dateLabel.Text = $"Election Date: {dateDisplayText} | Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading election header: {ex.Message}", "Header Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void LoadPositionHeader(string positionTitle, string electionTitle = "")
        {
            try
            {
                // Update for position-specific header
                if (!string.IsNullOrEmpty(electionTitle))
                {
                    titleLabel.Text = electionTitle.ToUpper();
                    subtitleLabel.Text = $"POSITION: {positionTitle.ToUpper()}";
                }
                else
                {
                    titleLabel.Text = "UniVoting Election System";
                    subtitleLabel.Text = $"POSITION: {positionTitle.ToUpper()}";
                }
                
                dateLabel.Text = $"Vote Count Results | Generated: {DateTime.Now:yyyy-MM-dd HH:mm}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading position header: {ex.Message}", "Header Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void RefreshHeader()
        {
            try
            {
                // Update the generation time
                var currentText = dateLabel.Text;
                if (currentText.Contains("Generated:"))
                {
                    var parts = currentText.Split('|');
                    if (parts.Length > 0)
                    {
                        dateLabel.Text = $"{parts[0].Trim()} | Generated: {DateTime.Now:yyyy-MM-dd HH:mm}";
                    }
                }
                
                headerPanel.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing header: {ex.Message}", "Header Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        public void ExportHeaderToPdf(string filePath)
        {
            try
            {
                MessageBox.Show("Header PDF export will be available with FastReport templates.", 
                    "Export PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting header to PDF: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void SetCustomTitle(string title)
        {
            titleLabel.Text = title;
        }
        
        public void SetCustomSubtitle(string subtitle)
        {
            subtitleLabel.Text = subtitle;
        }
        
        public void SetInstitution(string institution)
        {
            if (!string.IsNullOrEmpty(institution))
            {
                titleLabel.Text = institution.ToUpper();
            }
        }
    }
}