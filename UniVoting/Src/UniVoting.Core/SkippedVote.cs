namespace UniVoting.Core
{
    /// <summary>
    /// A class which represents the SkippedVotes table.
    /// </summary>
    public partial class SkippedVote
    {

        public virtual int Id { get; set; }
        public virtual int Positionid { get; set; }
        public virtual int VoterId { get; set; }
        public virtual Position Position { get; set; }
        public virtual Voter Voter { get; set; }
    }
}