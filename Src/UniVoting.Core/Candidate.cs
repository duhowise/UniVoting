namespace Univoting.Core
{
    /// <summary>
    /// A class which represents the Candidate table.
    /// </summary>
    public partial class Candidate
    {
        public virtual int Id { get; set; }
        public virtual int? PositionId { get; set; }
        public virtual string CandidateName { get; set; }
        public virtual byte[] CandidatePicture { get; set; }
        public virtual int? RankId { get; set; }
        public virtual Position Position { get; set; }
        public virtual Rank Rank { get; set; }

		
	}
}