namespace VoteReport
{
    partial class VoteCountReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoteCountReport));
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule5 = new Telerik.Reporting.Drawing.StyleRule();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.positionDatasource = new Telerik.Reporting.SqlDataSource();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.voteCaptionTextBox = new Telerik.Reporting.TextBox();
            this.positionCaptionTextBox = new Telerik.Reporting.TextBox();
            this.nameCaptionTextBox = new Telerik.Reporting.TextBox();
            this.VoteDataSource = new Telerik.Reporting.SqlDataSource();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.reportNameTextBox = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.position = new Telerik.Reporting.TextBox();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.voteDataTextBox = new Telerik.Reporting.TextBox();
            this.positionDataTextBox = new Telerik.Reporting.TextBox();
            this.nameDataTextBox = new Telerik.Reporting.TextBox();
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
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.364268034696579D);
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // positionDatasource
            // 
            this.positionDatasource.ConnectionString = "VoteReport.Properties.Settings.VotingSystem";
            this.positionDatasource.Name = "positionDatasource";
            this.positionDatasource.SelectCommand = "SELECT Position.PositionName FROM Position";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.1459121704101562D), Telerik.Reporting.Drawing.Unit.Inch(1.2187894582748413D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.6540091037750244D), Telerik.Reporting.Drawing.Unit.Inch(0.28117120265960693D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Picture";
            // 
            // voteCaptionTextBox
            // 
            this.voteCaptionTextBox.CanGrow = true;
            this.voteCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(6.2088173491403609E-10D), Telerik.Reporting.Drawing.Unit.Inch(1.2187894582748413D));
            this.voteCaptionTextBox.Name = "voteCaptionTextBox";
            this.voteCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.1458332538604736D), Telerik.Reporting.Drawing.Unit.Inch(0.28117120265960693D));
            this.voteCaptionTextBox.StyleName = "Caption";
            this.voteCaptionTextBox.Value = "Name";
            // 
            // positionCaptionTextBox
            // 
            this.positionCaptionTextBox.CanGrow = true;
            this.positionCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.8000001907348633D), Telerik.Reporting.Drawing.Unit.Inch(1.2187894582748413D));
            this.positionCaptionTextBox.Name = "positionCaptionTextBox";
            this.positionCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999210596084595D), Telerik.Reporting.Drawing.Unit.Inch(0.281131774187088D));
            this.positionCaptionTextBox.StyleName = "Caption";
            this.positionCaptionTextBox.Value = "Vote";
            // 
            // nameCaptionTextBox
            // 
            this.nameCaptionTextBox.CanGrow = true;
            this.nameCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(1.2187894582748413D));
            this.nameCaptionTextBox.Name = "nameCaptionTextBox";
            this.nameCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2583332061767578D), Telerik.Reporting.Drawing.Unit.Inch(0.281131774187088D));
            this.nameCaptionTextBox.StyleName = "Caption";
            this.nameCaptionTextBox.Value = "Percentage Votes";
            // 
            // VoteDataSource
            // 
            this.VoteDataSource.ConnectionString = "VoteReport.Properties.Settings.VotingSystem";
            this.VoteDataSource.Name = "VoteDataSource";
            this.VoteDataSource.SelectCommand = resources.GetString("VoteDataSource.SelectCommand");
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.3000000715255737D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.reportNameTextBox});
            this.pageHeader.Name = "pageHeader";
            // 
            // reportNameTextBox
            // 
            this.reportNameTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.reportNameTextBox.Name = "reportNameTextBox";
            this.reportNameTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4166665077209473D), Telerik.Reporting.Drawing.Unit.Inch(1.2791666984558106D));
            this.reportNameTextBox.StyleName = "PageInfo";
            this.reportNameTextBox.Value = "VoteCountReport";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.35640716552734375D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9791669845581055D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=NOW()";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9000790119171143D), Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.5374209880828857D), Telerik.Reporting.Drawing.Unit.Inch(0.20000000298023224D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "=PageNumber";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.4999605417251587D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.textBox1,
            this.voteCaptionTextBox,
            this.positionCaptionTextBox,
            this.nameCaptionTextBox,
            this.position});
            this.reportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.9418537198798731E-05D), Telerik.Reporting.Drawing.Unit.Inch(7.8837074397597462E-05D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.4583334922790527D), Telerik.Reporting.Drawing.Unit.Inch(1.2186316251754761D));
            this.titleTextBox.Style.Font.Name = "Segoe UI";
            this.titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(22D);
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "                \t APEES ELECTION 2015/2016";
            // 
            // position
            // 
            this.position.CanGrow = true;
            this.position.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(1.5D), Telerik.Reporting.Drawing.Unit.Inch(0.5D));
            this.position.Name = "position";
            this.position.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(4.9375D), Telerik.Reporting.Drawing.Unit.Inch(0.50000005960464478D));
            this.position.Style.Font.Name = "Segoe UI";
            this.position.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.position.StyleName = "Caption";
            this.position.Value = "= Fields.Position";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.37724080681800842D);
            this.reportFooter.Name = "reportFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Inch(1.7357710599899292D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.voteDataTextBox,
            this.positionDataTextBox,
            this.nameDataTextBox,
            this.pictureBox1});
            this.detail.Name = "detail";
            // 
            // voteDataTextBox
            // 
            this.voteDataTextBox.CanGrow = true;
            this.voteDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.02083333395421505D), Telerik.Reporting.Drawing.Unit.Inch(0.13573186099529266D));
            this.voteDataTextBox.Name = "voteDataTextBox";
            this.voteDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(2.125D), Telerik.Reporting.Drawing.Unit.Inch(0.35640716552734375D));
            this.voteDataTextBox.Style.Font.Name = "Segoe UI";
            this.voteDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(16D);
            this.voteDataTextBox.StyleName = "Data";
            this.voteDataTextBox.Value = "= Fields.Name";
            // 
            // positionDataTextBox
            // 
            this.positionDataTextBox.CanGrow = true;
            this.positionDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.8000001907348633D), Telerik.Reporting.Drawing.Unit.Inch(0.13573186099529266D));
            this.positionDataTextBox.Name = "positionDataTextBox";
            this.positionDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.3999210596084595D), Telerik.Reporting.Drawing.Unit.Inch(0.35640716552734375D));
            this.positionDataTextBox.Style.Font.Name = "Segoe UI";
            this.positionDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.positionDataTextBox.StyleName = "Data";
            this.positionDataTextBox.Value = "= Fields.Vote";
            // 
            // nameDataTextBox
            // 
            this.nameDataTextBox.CanGrow = true;
            this.nameDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(5.2000002861022949D), Telerik.Reporting.Drawing.Unit.Inch(0.13573186099529266D));
            this.nameDataTextBox.Name = "nameDataTextBox";
            this.nameDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.2374998331069946D), Telerik.Reporting.Drawing.Unit.Inch(0.35640716552734375D));
            this.nameDataTextBox.Style.Font.Name = "Segoe UI";
            this.nameDataTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            this.nameDataTextBox.StyleName = "Data";
            this.nameDataTextBox.Value = "= Fields.[Percentage Votes]";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Bindings.Add(new Telerik.Reporting.Binding("Value", "= Fields.CandidatePicture"));
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(2.1459121704101562D), Telerik.Reporting.Drawing.Unit.Inch(3.9582449971931055E-05D));
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(1.654009222984314D), Telerik.Reporting.Drawing.Unit.Inch(1.7357313632965088D));
            // 
            // VoteCountReport
            // 
            this.DataSource = this.VoteDataSource;
            this.DocumentName = "= Fields.Position";
            this.Filters.Add(new Telerik.Reporting.Filter("= Fields.Position", Telerik.Reporting.FilterOperator.In, "= Parameters.positionSelect.Value"));
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "VoteCountReport";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D), Telerik.Reporting.Drawing.Unit.Inch(1D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.positionDatasource;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.PositionName";
            reportParameter1.AvailableValues.ValueMember = "= Fields.PositionName";
            reportParameter1.MultiValue = true;
            reportParameter1.Name = "positionSelect";
            reportParameter1.Text = "select position";
            reportParameter1.Value = "President";
            reportParameter1.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.Style.Font.Bold = true;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Title")});
            styleRule2.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(222)))), ((int)(((byte)(201)))));
            styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(39)))), ((int)(((byte)(28)))));
            styleRule2.Style.Font.Name = "Gill Sans MT";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18D);
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Caption")});
            styleRule3.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(222)))), ((int)(((byte)(201)))));
            styleRule3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(29)))), ((int)(((byte)(20)))));
            styleRule3.Style.Font.Name = "Gill Sans MT";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("Data")});
            styleRule4.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(29)))), ((int)(((byte)(20)))));
            styleRule4.Style.Font.Name = "Gill Sans MT";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            styleRule4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            styleRule5.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector("PageInfo")});
            styleRule5.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(202)))), ((int)(((byte)(189)))));
            styleRule5.Style.Font.Name = "Gill Sans MT";
            styleRule5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            styleRule5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4,
            styleRule5});
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(6.4999604225158691D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.SqlDataSource VoteDataSource;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.TextBox voteCaptionTextBox;
        private Telerik.Reporting.TextBox positionCaptionTextBox;
        private Telerik.Reporting.TextBox nameCaptionTextBox;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.TextBox reportNameTextBox;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox voteDataTextBox;
        private Telerik.Reporting.TextBox positionDataTextBox;
        private Telerik.Reporting.TextBox nameDataTextBox;
        private Telerik.Reporting.SqlDataSource positionDatasource;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox position;
    }
}