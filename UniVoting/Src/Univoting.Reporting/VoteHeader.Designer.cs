using Univoting.Reporting.Properties;

namespace Univoting.Reporting
{
    partial class VoteHeader
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
			this.detail = new Telerik.Reporting.DetailSection();
			this.textBox1 = new Telerik.Reporting.TextBox();
			this.textBox2 = new Telerik.Reporting.TextBox();
			this.sqlDataSource1 = new Telerik.Reporting.SqlDataSource();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// detail
			// 
			this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(1.1000000238418579D);
			this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2});
			this.detail.Name = "detail";
			// 
			// textBox1
			// 
			this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(0.10000002384185791D));
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0999221801757812D), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481D));
			this.textBox1.Style.Font.Bold = true;
			this.textBox1.Style.Font.Name = "Segoe UI";
			this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(22D);
			this.textBox1.Style.LineColor = System.Drawing.Color.SteelBlue;
			this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
			this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
			this.textBox1.Value = "= Fields.ElectionName";
			// 
			// textBox2
			// 
			this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0D), Telerik.Reporting.Drawing.Unit.Inch(0.60000008344650269D));
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.0999221801757812D), Telerik.Reporting.Drawing.Unit.Inch(0.29996064305305481D));
			this.textBox2.Style.Font.Bold = true;
			this.textBox2.Style.Font.Name = "Segoe UI";
			this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(15D);
			this.textBox2.Style.LineColor = System.Drawing.Color.SteelBlue;
			this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
			this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
			this.textBox2.Value = "= Fields.EletionSubTitle";
			// 
			// sqlDataSource1
			// 
			this.sqlDataSource1.ConnectionString = "Univoting.Reporting.Properties.Settings.VotingSystemV2";
			this.sqlDataSource1.Name = "sqlDataSource1";
			this.sqlDataSource1.SelectCommand = "select * FROM Settings s WHERE s.id=1008";
			// 
			// VoteHeader
			// 
			this.DataSource = this.sqlDataSource1;
			this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
			this.Name = "VoteHeader";
			this.PageSettings.Landscape = false;
			this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
			this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
			this.PageSettings.PaperSize = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4200000762939453D), Telerik.Reporting.Drawing.Unit.Inch(0.699999988079071D));
			styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
			styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
			styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
			this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
			this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.1000008583068848D);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.SqlDataSource sqlDataSource1;
    }
}