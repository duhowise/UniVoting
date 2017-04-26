namespace Univoting.Reporting
{
    partial class VoteReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoteReport));
			Telerik.Reporting.TypeReportSource typeReportSource1 = new Telerik.Reporting.TypeReportSource();
			Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
			Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
			Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
			Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
			Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
			Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
			Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
			Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
			this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
			this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
			this.positionCaptionTextBox = new Telerik.Reporting.TextBox();
			this.nameCaptionTextBox = new Telerik.Reporting.TextBox();
			this.candidatePictureCaptionTextBox = new Telerik.Reporting.TextBox();
			this.voteCaptionTextBox = new Telerik.Reporting.TextBox();
			this.textBox1 = new Telerik.Reporting.TextBox();
			this.positionGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
			this.positionGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
			this.textBox2 = new Telerik.Reporting.TextBox();
			this.PositionDataSource = new Telerik.Reporting.SqlDataSource();
			this.VotingDataSource = new Telerik.Reporting.SqlDataSource();
			this.pageHeader = new Telerik.Reporting.PageHeaderSection();
			this.reportNameTextBox = new Telerik.Reporting.TextBox();
			this.pageFooter = new Telerik.Reporting.PageFooterSection();
			this.currentTimeTextBox = new Telerik.Reporting.TextBox();
			this.pageInfoTextBox = new Telerik.Reporting.TextBox();
			this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
			this.positionCaptionTextBox1 = new Telerik.Reporting.TextBox();
			this.positionDataTextBox = new Telerik.Reporting.TextBox();
			this.subReport1 = new Telerik.Reporting.SubReport();
			this.reportFooter = new Telerik.Reporting.ReportFooterSection();
			this.detail = new Telerik.Reporting.DetailSection();
			this.nameDataTextBox = new Telerik.Reporting.TextBox();
			this.voteDataTextBox = new Telerik.Reporting.TextBox();
			this.textBox3 = new Telerik.Reporting.TextBox();
			this.pictureBox1 = new Telerik.Reporting.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// labelsGroupFooterSection
			// 
			this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
			this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
			this.labelsGroupFooterSection.Style.Visible = false;
			// 
			// labelsGroupHeaderSection
			// 
			this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
			this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.positionCaptionTextBox,
            this.nameCaptionTextBox,
            this.candidatePictureCaptionTextBox,
            this.voteCaptionTextBox,
            this.textBox1});
			this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
			this.labelsGroupHeaderSection.PrintOnEveryPage = true;
			// 
			// positionCaptionTextBox
			// 
			this.positionCaptionTextBox.CanGrow = true;
			this.positionCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.positionCaptionTextBox.Name = "positionCaptionTextBox";
			this.positionCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.positionCaptionTextBox.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(18D);
			this.positionCaptionTextBox.StyleName = "Caption";
			this.positionCaptionTextBox.Value = "Position";
			// 
			// nameCaptionTextBox
			// 
			this.nameCaptionTextBox.CanGrow = true;
			this.nameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3083332777023315D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.nameCaptionTextBox.Name = "nameCaptionTextBox";
			this.nameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.nameCaptionTextBox.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(18D);
			this.nameCaptionTextBox.StyleName = "Caption";
			this.nameCaptionTextBox.Value = "Name";
			// 
			// candidatePictureCaptionTextBox
			// 
			this.candidatePictureCaptionTextBox.CanGrow = true;
			this.candidatePictureCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.5958333015441895D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.candidatePictureCaptionTextBox.Name = "candidatePictureCaptionTextBox";
			this.candidatePictureCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.candidatePictureCaptionTextBox.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(18D);
			this.candidatePictureCaptionTextBox.StyleName = "Caption";
			this.candidatePictureCaptionTextBox.Value = "Candidate Picture";
			// 
			// voteCaptionTextBox
			// 
			this.voteCaptionTextBox.CanGrow = true;
			this.voteCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.8833334445953369D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.voteCaptionTextBox.Name = "voteCaptionTextBox";
			this.voteCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.voteCaptionTextBox.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(18D);
			this.voteCaptionTextBox.StyleName = "Caption";
			this.voteCaptionTextBox.Value = "Vote";
			// 
			// textBox1
			// 
			this.textBox1.CanGrow = true;
			this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1708331108093262D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.textBox1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(18D);
			this.textBox1.StyleName = "Caption";
			this.textBox1.Value = "Percentage Votes";
			// 
			// positionGroupFooterSection
			// 
			this.positionGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
			this.positionGroupFooterSection.Name = "positionGroupFooterSection";
			// 
			// positionGroupHeaderSection
			// 
			this.positionGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
			this.positionGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2});
			this.positionGroupHeaderSection.Name = "positionGroupHeaderSection";
			// 
			// textBox2
			// 
			this.textBox2.CanGrow = true;
			this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.textBox2.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(18D);
			this.textBox2.StyleName = "Data";
			this.textBox2.Value = "= Fields.Position";
			// 
			// PositionDataSource
			// 
			this.PositionDataSource.ConnectionString = "Univoting.Reporting.Properties.Settings.VotingSystemV2";
			this.PositionDataSource.Name = "PositionDataSource";
			this.PositionDataSource.SelectCommand = "SELECT PositionName FROM dbo.Position";
			// 
			// VotingDataSource
			// 
			this.VotingDataSource.ConnectionString = "Data Source=localhost;Initial Catalog=VotingSystemV2;User ID=sa;Password=23m@y199" +
    "3";
			this.VotingDataSource.Name = "VotingDataSource";
			this.VotingDataSource.ProviderName = "System.Data.SqlClient";
			this.VotingDataSource.SelectCommand = resources.GetString("VotingDataSource.SelectCommand");
			// 
			// pageHeader
			// 
			this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
			this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.reportNameTextBox});
			this.pageHeader.Name = "pageHeader";
			// 
			// reportNameTextBox
			// 
			this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.reportNameTextBox.Name = "reportNameTextBox";
			this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4166665077209473D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.reportNameTextBox.StyleName = "PageInfo";
			this.reportNameTextBox.Value = "BestSoft World Software - Electronic Voting Services ";
			// 
			// pageFooter
			// 
			this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
			this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
			this.pageFooter.Name = "pageFooter";
			// 
			// currentTimeTextBox
			// 
			this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.currentTimeTextBox.Name = "currentTimeTextBox";
			this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.currentTimeTextBox.StyleName = "PageInfo";
			this.currentTimeTextBox.Value = "=NOW()";
			// 
			// pageInfoTextBox
			// 
			this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2395832538604736D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
			this.pageInfoTextBox.Name = "pageInfoTextBox";
			this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
			this.pageInfoTextBox.StyleName = "PageInfo";
			this.pageInfoTextBox.Value = "=PageNumber";
			// 
			// reportHeader
			// 
			this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.6187500953674316D);
			this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.positionCaptionTextBox1,
            this.positionDataTextBox,
            this.subReport1});
			this.reportHeader.Name = "reportHeader";
			// 
			// positionCaptionTextBox1
			// 
			this.positionCaptionTextBox1.CanGrow = true;
			this.positionCaptionTextBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
			this.positionCaptionTextBox1.Name = "positionCaptionTextBox1";
			this.positionCaptionTextBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.positionCaptionTextBox1.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(18D);
			this.positionCaptionTextBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
			this.positionCaptionTextBox1.StyleName = "Caption";
			this.positionCaptionTextBox1.Value = "Position:";
			// 
			// positionDataTextBox
			// 
			this.positionDataTextBox.CanGrow = true;
			this.positionDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.2395832538604736D), Telerik.Reporting.Drawing.Unit.Inch(0.80823493003845215D));
			this.positionDataTextBox.Name = "positionDataTextBox";
			this.positionDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3.1979167461395264D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.positionDataTextBox.Style.LineWidth = Telerik.Reporting.Drawing.Unit.Point(18D);
			this.positionDataTextBox.StyleName = "Data";
			this.positionDataTextBox.Value = "= Fields.Position";
			// 
			// subReport1
			// 
			this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.041667301207780838D), Telerik.Reporting.Drawing.Unit.Inch(0D));
			this.subReport1.Name = "subReport1";
			typeReportSource1.TypeName = "Univoting.Reporting.VoteHeader, Univoting.Reporting, Version=1.0.0.0, Culture=neu" +
    "tral, PublicKeyToken=null";
			this.subReport1.ReportSource = typeReportSource1;
			this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4166660308837891D), Telerik.Reporting.Drawing.Unit.Inch(0.78736215829849243D));
			// 
			// reportFooter
			// 
			this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.28125D);
			this.reportFooter.Name = "reportFooter";
			// 
			// detail
			// 
			this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(1.2334905862808228D);
			this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.nameDataTextBox,
            this.voteDataTextBox,
            this.textBox3,
            this.pictureBox1});
			this.detail.Name = "detail";
			// 
			// nameDataTextBox
			// 
			this.nameDataTextBox.CanGrow = true;
			this.nameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.3083332777023315D), Telerik.Reporting.Drawing.Unit.Inch(0.32718181610107422D));
			this.nameDataTextBox.Name = "nameDataTextBox";
			this.nameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.nameDataTextBox.StyleName = "Data";
			this.nameDataTextBox.Value = "= Fields.Name";
			// 
			// voteDataTextBox
			// 
			this.voteDataTextBox.CanGrow = true;
			this.voteDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.8833332061767578D), Telerik.Reporting.Drawing.Unit.Inch(0.32718214392662048D));
			this.voteDataTextBox.Name = "voteDataTextBox";
			this.voteDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.voteDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
			this.voteDataTextBox.StyleName = "Data";
			this.voteDataTextBox.Value = "= Fields.Vote";
			// 
			// textBox3
			// 
			this.textBox3.CanGrow = true;
			this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.1916670799255371D), Telerik.Reporting.Drawing.Unit.Inch(0.32718181610107422D));
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2666666507720947D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
			this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
			this.textBox3.StyleName = "Data";
			this.textBox3.Value = "= Fields.[Percentage Votes]";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Bindings.Add(new Telerik.Reporting.Binding("Value", "= Fields.CandidatePicture"));
			this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.7000000476837158D), Telerik.Reporting.Drawing.Unit.Inch(0.12718169391155243D));
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.1625000238418579D), Telerik.Reporting.Drawing.Unit.Inch(1.1063089370727539D));
			this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
			// 
			// VoteReport
			// 
			this.DataSource = this.VotingDataSource;
			this.Filters.Add(new Telerik.Reporting.Filter("= Fields.Position", Telerik.Reporting.FilterOperator.In, "= Parameters.PositionSelect.Value"));
			group1.GroupFooter = this.labelsGroupFooterSection;
			group1.GroupHeader = this.labelsGroupHeaderSection;
			group1.Name = "labelsGroup";
			group2.GroupFooter = this.positionGroupFooterSection;
			group2.GroupHeader = this.positionGroupHeaderSection;
			group2.Groupings.Add(new Telerik.Reporting.Grouping("= Fields.Position"));
			group2.Name = "positionGroup";
			this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
			this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.positionGroupHeaderSection,
            this.positionGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
			this.Name = "VoteReport";
			this.PageSettings.Landscape = false;
			this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
			this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
			reportParameter1.AutoRefresh = true;
			reportParameter1.AvailableValues.DataSource = this.PositionDataSource;
			reportParameter1.AvailableValues.DisplayMember = "= Fields.PositionName";
			reportParameter1.AvailableValues.ValueMember = "= Fields.PositionName";
			reportParameter1.MultiValue = true;
			reportParameter1.Name = "PositionSelect";
			reportParameter1.Text = "Select a Position";
			reportParameter1.Value = "position";
			reportParameter1.Visible = true;
			this.ReportParameters.Add(reportParameter1);
			styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
			styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
			styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
			styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
			styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
			styleRule2.Style.Font.Name = "Calibri";
			styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
			styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
			styleRule3.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(167)))), ((int)(((byte)(227)))));
			styleRule3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
			styleRule3.Style.Font.Name = "Calibri";
			styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
			styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
			styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
			styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
			styleRule4.Style.Font.Name = "Calibri";
			styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
			styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
			styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
			styleRule5.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
			styleRule5.Style.Font.Name = "Calibri";
			styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
			styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
			this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
			this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.4583334922790527D);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource VotingDataSource;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox positionCaptionTextBox;
        private Telerik.Reporting.TextBox nameCaptionTextBox;
        private Telerik.Reporting.TextBox candidatePictureCaptionTextBox;
        private Telerik.Reporting.TextBox voteCaptionTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.GroupHeaderSection positionGroupHeaderSection;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.GroupFooterSection positionGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox positionCaptionTextBox1;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox nameDataTextBox;
        private Telerik.Reporting.TextBox voteDataTextBox;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox positionDataTextBox;
        private Telerik.Reporting.SqlDataSource PositionDataSource;
        private Telerik.Reporting.SubReport subReport1;
    }
}