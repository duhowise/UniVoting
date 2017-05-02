using System.Configuration;
using System.Data;

namespace Univoting.Reporting
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;
	using Telerik.Reporting;
	using Telerik.Reporting.Drawing;

	/// <summary>
	/// Summary description for HeadSection.
	/// </summary>
	public partial class HeadSection : Telerik.Reporting.Report
	{
		public HeadSection()
		{
			//
			// Required for telerik Reporting designer support
			//
			InitializeComponent();
			HeadSectionDataSource.Parameters.Add("@Id",DbType.Int32,ConfigurationManager.AppSettings["ElectionId"]);
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
	}
}