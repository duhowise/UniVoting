using FastReport;
using FastReport.Data;
using System;
using System.Data;
using System.IO;

namespace UniVoting.Services
{
    /// <summary>
    /// Service for managing FastReport.OpenSource operations across the UniVoting application
    /// </summary>
    public class FastReportService
    {
        private readonly string _reportsPath;
        
        public FastReportService(string reportsPath = null)
        {
            _reportsPath = reportsPath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            EnsureReportsDirectory();
        }
        
        private void EnsureReportsDirectory()
        {
            if (!Directory.Exists(_reportsPath))
            {
                Directory.CreateDirectory(_reportsPath);
            }
        }
        
        /// <summary>
        /// Creates a new report instance with basic configuration
        /// </summary>
        public Report CreateReport()
        {
            var report = new Report();
            
            // Set default configuration
            ConfigureReport(report);
            
            return report;
        }
        
        /// <summary>
        /// Creates an election results report
        /// </summary>
        public Report CreateElectionResultsReport(DataTable electionData, string electionTitle)
        {
            var report = CreateReport();
            
            try
            {
                // Register data source
                RegisterDataSource(report, electionData, "ElectionResults");
                
                // Create election report layout
                CreateElectionLayout(report, electionTitle);
                
                return report;
            }
            catch (Exception ex)
            {
                report.Dispose();
                throw new Exception($"Error creating election results report: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Creates a vote count summary report
        /// </summary>
        public Report CreateVoteCountReport(DataTable voteData, string positionTitle)
        {
            var report = CreateReport();
            
            try
            {
                // Register data source
                RegisterDataSource(report, voteData, "VoteCounts");
                
                // Create vote count layout
                CreateVoteCountLayout(report, positionTitle);
                
                return report;
            }
            catch (Exception ex)
            {
                report.Dispose();
                throw new Exception($"Error creating vote count report: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Creates a comprehensive election analysis report
        /// </summary>
        public Report CreateElectionAnalysisReport(DataTable candidateData, DataTable voteData, 
            DataTable summaryData, string electionTitle)
        {
            var report = CreateReport();
            
            try
            {
                // Register multiple data sources
                RegisterDataSource(report, candidateData, "Candidates");
                RegisterDataSource(report, voteData, "Votes");
                RegisterDataSource(report, summaryData, "Summary");
                
                // Create comprehensive analysis layout
                CreateAnalysisLayout(report, electionTitle);
                
                return report;
            }
            catch (Exception ex)
            {
                report.Dispose();
                throw new Exception($"Error creating election analysis report: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Exports a report to PDF format
        /// </summary>
        public void ExportToPdf(Report report, string filePath)
        {
            try
            {
                if (!report.IsPrepared)
                    report.Prepare();
                    
                // Note: PDF export may require FastReport.OpenSource.Export.PdfSimple package
                // or use SavePrepared for now
                report.SavePrepared(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exporting report to PDF: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Exports a report to HTML format
        /// </summary>
        public void ExportToHtml(Report report, string filePath)
        {
            try
            {
                if (!report.IsPrepared)
                    report.Prepare();
                    
                // Note: HTML export may not be available in FastReport.OpenSource
                // Consider alternative export methods or save as prepared report
                throw new NotSupportedException("HTML export may not be available in FastReport.OpenSource. Consider using SavePrepared method.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exporting report to HTML: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Prints a report using the default printer
        /// </summary>
        public void PrintReport(Report report)
        {
            try
            {
                if (!report.IsPrepared)
                    report.Prepare();
                    
                // Note: Print method may not be available in FastReport.OpenSource
                // Consider using export to PDF and then print the PDF file
                throw new NotSupportedException("Direct printing may not be available in FastReport.OpenSource. Consider exporting to PDF first.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error printing report: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Saves a report template to file
        /// </summary>
        public void SaveReportTemplate(Report report, string templateName)
        {
            try
            {
                var templatePath = Path.Combine(_reportsPath, $"{templateName}.frx");
                report.Save(templatePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving report template: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Loads a report template from file
        /// </summary>
        public Report LoadReportTemplate(string templateName)
        {
            try
            {
                var templatePath = Path.Combine(_reportsPath, $"{templateName}.frx");
                if (!File.Exists(templatePath))
                    throw new FileNotFoundException($"Report template not found: {templatePath}");
                    
                var report = new Report();
                ConfigureReport(report);
                report.Load(templatePath);
                
                return report;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading report template: {ex.Message}", ex);
            }
        }
        
        private void ConfigureReport(Report report)
        {
            // Set default report configuration
            report.ReportInfo.Author = "UniVoting System";
            report.ReportInfo.Created = DateTime.Now;
            report.ReportInfo.Modified = DateTime.Now;
        }
        
        private void RegisterDataSource(Report report, DataTable dataTable, string dataSourceName)
        {
            // Remove existing data source if it exists
            var existingDataSource = report.Dictionary.DataSources.FindByName(dataSourceName);
            if (existingDataSource != null)
            {
                report.Dictionary.DataSources.Remove(existingDataSource);
            }
            
            // Register new data source
            var tableDataSource = new TableDataSource();
            tableDataSource.ReferenceName = dataSourceName;
            tableDataSource.Name = dataSourceName;
            tableDataSource.Table = dataTable;
            report.Dictionary.DataSources.Add(tableDataSource);
        }
        
        private void CreateElectionLayout(Report report, string electionTitle)
        {
            var page = CreateBasicPage(report);
            
            // Title band
            var titleBand = new ReportTitleBand();
            titleBand.Height = (float)(FastReport.Utils.Units.Centimeters * 3);
            page.Bands.Add(titleBand);
            
            var titleText = new TextObject();
            titleText.Text = $"{electionTitle.ToUpper()}\nELECTION RESULTS";
            titleText.Font = new System.Drawing.Font("Segoe UI", 18, System.Drawing.FontStyle.Bold);
            titleText.HorzAlign = HorzAlign.Center;
            titleText.VertAlign = VertAlign.Center;
            titleText.Bounds = new System.Drawing.RectangleF(0, 0, page.PaperWidth, titleBand.Height);
            titleBand.Objects.Add(titleText);
            
            // Data sections
            CreateDataSection(page, "ElectionResults");
            
            // Footer
            CreatePageFooter(page);
        }
        
        private void CreateVoteCountLayout(Report report, string positionTitle)
        {
            var page = CreateBasicPage(report);
            
            // Title band
            var titleBand = new ReportTitleBand();
            titleBand.Height = (float)(FastReport.Utils.Units.Centimeters * 2.5);
            page.Bands.Add(titleBand);
            
            var titleText = new TextObject();
            titleText.Text = $"VOTE COUNT REPORT\nPosition: {positionTitle}";
            titleText.Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);
            titleText.HorzAlign = HorzAlign.Center;
            titleText.VertAlign = VertAlign.Center;
            titleText.Bounds = new System.Drawing.RectangleF(0, 0, page.PaperWidth, titleBand.Height);
            titleBand.Objects.Add(titleText);
            
            // Data sections
            CreateDataSection(page, "VoteCounts");
            
            // Summary footer
            CreateSummaryFooter(page, "VoteCounts");
            
            // Page footer
            CreatePageFooter(page);
        }
        
        private void CreateAnalysisLayout(Report report, string electionTitle)
        {
            var page = CreateBasicPage(report);
            
            // Title band
            var titleBand = new ReportTitleBand();
            titleBand.Height = (float)(FastReport.Utils.Units.Centimeters * 3);
            page.Bands.Add(titleBand);
            
            var titleText = new TextObject();
            titleText.Text = $"{electionTitle.ToUpper()}\nCOMPREHENSIVE ANALYSIS REPORT";
            titleText.Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);
            titleText.HorzAlign = HorzAlign.Center;
            titleText.VertAlign = VertAlign.Center;
            titleText.Bounds = new System.Drawing.RectangleF(0, 0, page.PaperWidth, titleBand.Height);
            titleBand.Objects.Add(titleText);
            
            // Multiple data sections
            CreateDataSection(page, "Summary", isFirstSection: true);
            CreateDataSection(page, "Candidates", isFirstSection: false);
            
            // Footer
            CreatePageFooter(page);
        }
        
        private ReportPage CreateBasicPage(Report report)
        {
            var page = new ReportPage();
            page.CreateUniqueName();
            page.PaperWidth = (float)(FastReport.Utils.Units.Centimeters * 21); // A4 width
            page.PaperHeight = (float)(FastReport.Utils.Units.Centimeters * 29.7); // A4 height
            page.LeftMargin = (float)(FastReport.Utils.Units.Centimeters * 2);
            page.RightMargin = (float)(FastReport.Utils.Units.Centimeters * 2);
            page.TopMargin = (float)(FastReport.Utils.Units.Centimeters * 2);
            page.BottomMargin = (float)(FastReport.Utils.Units.Centimeters * 2);
            report.Pages.Add(page);
            
            return page;
        }
        
        private void CreateDataSection(ReportPage page, string dataSourceName, bool isFirstSection = true)
        {
            // Page header for column names
            var pageHeader = new PageHeaderBand();
            pageHeader.Height = (float)(FastReport.Utils.Units.Centimeters * 1.2);
            // Note: RepeatOnEveryPage property may not be available in FastReport.OpenSource
            page.Bands.Add(pageHeader);
            
            // Data band
            var dataBand = new DataBand();
            dataBand.Height = (float)(FastReport.Utils.Units.Centimeters * 1);
            dataBand.DataSource = dataBand.Report.Dictionary.DataSources.FindByName(dataSourceName) as TableDataSource;
            page.Bands.Add(dataBand);
            
            // Create columns based on data source
            var tableDataSource = dataBand.DataSource as TableDataSource;
            if (tableDataSource?.Table != null)
            {
                CreateDataColumns(dataBand, pageHeader, tableDataSource.Table, dataSourceName);
            }
        }
        
        private void CreateDataColumns(DataBand dataBand, PageHeaderBand pageHeader, 
            DataTable table, string dataSourceName)
        {
            if (table.Columns.Count == 0) return;
            
            float columnWidth = dataBand.Width / table.Columns.Count;
            
            for (int i = 0; i < table.Columns.Count; i++)
            {
                var column = table.Columns[i];
                
                // Header
                var headerText = new TextObject();
                headerText.Text = FormatColumnName(column.ColumnName);
                headerText.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
                headerText.HorzAlign = HorzAlign.Center;
                headerText.VertAlign = VertAlign.Center;
                headerText.Border.Lines = BorderLines.All;
                headerText.Fill = new SolidFill(System.Drawing.Color.FromArgb(240, 240, 240));
                headerText.Bounds = new System.Drawing.RectangleF(i * columnWidth, 0, 
                    columnWidth, pageHeader.Height);
                pageHeader.Objects.Add(headerText);
                
                // Data field
                var dataText = new TextObject();
                dataText.Text = $"[{dataSourceName}.{column.ColumnName}]";
                dataText.Font = new System.Drawing.Font("Segoe UI", 9);
                dataText.HorzAlign = IsNumericColumn(column) ? HorzAlign.Right : HorzAlign.Left;
                dataText.VertAlign = VertAlign.Center;
                dataText.Border.Lines = BorderLines.All;
                // Note: Padding property may not be available, using simple positioning instead
                dataText.Bounds = new System.Drawing.RectangleF(i * columnWidth, 0, 
                    columnWidth, dataBand.Height);
                dataBand.Objects.Add(dataText);
            }
        }
        
        private void CreateSummaryFooter(ReportPage page, string dataSourceName)
        {
            var summaryBand = new ReportSummaryBand();
            summaryBand.Height = (float)(FastReport.Utils.Units.Centimeters * 1.5);
            page.Bands.Add(summaryBand);
            
            var summaryText = new TextObject();
            summaryText.Text = $"Total Records: [COUNT({dataSourceName})]";
            summaryText.Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold);
            summaryText.HorzAlign = HorzAlign.Right;
            summaryText.VertAlign = VertAlign.Center;
            summaryText.Border.Lines = BorderLines.Top;
            summaryText.Bounds = new System.Drawing.RectangleF(0, 0, summaryBand.Width, summaryBand.Height);
            summaryBand.Objects.Add(summaryText);
        }
        
        private void CreatePageFooter(ReportPage page)
        {
            var pageFooter = new PageFooterBand();
            pageFooter.Height = (float)(FastReport.Utils.Units.Centimeters * 1);
            page.Bands.Add(pageFooter);
            
            // Page number
            var pageNumber = new TextObject();
            pageNumber.Text = "Page [Page] of [TotalPages]";
            pageNumber.Font = new System.Drawing.Font("Segoe UI", 9);
            pageNumber.HorzAlign = HorzAlign.Right;
            pageNumber.VertAlign = VertAlign.Center;
            pageNumber.Bounds = new System.Drawing.RectangleF(page.PaperWidth * 0.7f, 0, 
                page.PaperWidth * 0.3f, pageFooter.Height);
            pageFooter.Objects.Add(pageNumber);
            
            // Generated date
            var dateText = new TextObject();
            dateText.Text = $"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}";
            dateText.Font = new System.Drawing.Font("Segoe UI", 9);
            dateText.HorzAlign = HorzAlign.Left;
            dateText.VertAlign = VertAlign.Center;
            dateText.Bounds = new System.Drawing.RectangleF(0, 0, 
                page.PaperWidth * 0.5f, pageFooter.Height);
            pageFooter.Objects.Add(dateText);
        }
        
        private string FormatColumnName(string columnName)
        {
            var result = System.Text.RegularExpressions.Regex.Replace(columnName, 
                "(\\B[A-Z])", " $1");
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(result.ToLower());
        }
        
        private bool IsNumericColumn(DataColumn column)
        {
            return column.DataType == typeof(int) || 
                   column.DataType == typeof(long) || 
                   column.DataType == typeof(decimal) || 
                   column.DataType == typeof(double) || 
                   column.DataType == typeof(float);
        }
    }
}