using System.Collections.Generic;

namespace UniVoting.Core
{
    /// <summary>
    /// A class which represents the Position table.
    /// </summary>
    public partial class Position
    {
        public virtual int Id { get; set; }
        public virtual string PositionName { get; set; }
        public Faculty Faculty { get; set; }
        public int FacultyId { get; set; }
        public Rank Rank { get; set; }
        public int RankId { get; set; }
        public virtual IEnumerable<Candidate> Candidates { get; set; }
        public virtual IEnumerable<SkippedVote> SkippedVotes { get; set; }
        public virtual IEnumerable<Vote> Votes { get; set; }
    }
}