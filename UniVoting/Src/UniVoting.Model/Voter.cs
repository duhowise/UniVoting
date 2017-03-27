using System.Collections.Generic;

namespace UniVoting.Model
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
        public virtual bool? Voted { get; set; }
        public virtual bool? VoteInProgress { get; set; }
        public virtual IEnumerable<Vote> Votes { get; set; }
    }
}