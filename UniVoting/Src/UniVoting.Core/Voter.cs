using System.Collections.Generic;

namespace UniVoting.Core
{
    /// <summary>
    /// A class which represents the Voter table.
    /// </summary>
    public partial class Voter
    {
        public Voter()
        {
        }
        public virtual int Id { get; set; }
        public virtual string VoterName { get; set; }
        public virtual string VoterCode { get; set; }
        public virtual string IndexNumber { get; set; }
        public Faculty Faculty { get; set; }
        public int FacultyId { get; set; }
        public virtual bool Voted { get; set; }
        public virtual bool VoteInProgress { get; set; }
        public virtual IEnumerable<Vote> Votes { get; set; }
    }

    public class Faculty
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
    }
}