using System.Collections.Generic;

namespace Univoting.Core
{
    /// <summary>
    /// A class which represents the Voter table.
    /// </summary>
    public partial class Voter
    {
       
        public virtual int Id { get; set; }
        public virtual string VoterName { get; set; }
        public virtual string VoterCode { get; set; }
        public virtual string IndexNumber { get; set; }
        public Faculty Faculty { get; set; }
        public int FacultyId { get; set; }
        public  bool Voted { get; set; }
        public  bool VoteInProgress { get; set; }
        public  ICollection<Vote> Votes { get; set; }
        public  ICollection<SkippedVote> SkippedVotes { get; set; }
    }
}