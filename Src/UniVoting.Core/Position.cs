using System.Collections.Generic;

namespace Univoting.Core
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
        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<SkippedVote> SkippedVotes { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}