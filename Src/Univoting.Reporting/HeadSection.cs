using System.Configuration;
using System.Data;

namespace Univoting.Reporting
{
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