using System.Collections.Generic;

namespace UniVoting.Model
{
    /// <summary>
    /// A class which represents the Rank table.
    /// </summary>
    public partial class Rank
    {
        public virtual int id { get; set; }
        public virtual byte _Rank { get; set; }
        public virtual IEnumerable<Candidate> Candidates { get; set; }
    }
}