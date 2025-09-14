using Dapper;

namespace UniVoting.Model
{
	/// <summary>
	/// A class which represents the Settings table.
	/// </summary>
	[Table("Settings")]
	public partial class Setting
	{
		public virtual int Id { get; set; }
		public virtual string ElectionName { get; set; }
		public virtual string EletionSubTitle { get; set; }
		public virtual byte[] Logo { get; set; }
		public virtual string Colour { get; set; }
	}
}