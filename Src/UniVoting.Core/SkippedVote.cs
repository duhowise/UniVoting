namespace Univoting.Core
{
    /// <summary>
    /// A class which represents the SkippedVotes table.
    /// </summary>
    public partial class SkippedVote
    {
        public  int Id { get; set; }
        public  int PositionId { get; set; }
        public int VoterId { get; set; }
        public virtual Position Position { get; set; }
        public virtual Voter Voter { get; set; }
    }
}