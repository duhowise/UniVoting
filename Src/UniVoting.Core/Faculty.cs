using System.Collections.Generic;

namespace Univoting.Core
{
    public class Faculty
    {
        public virtual int Id { get; set; }
        public virtual string FacultyName { get; set; }
        public ICollection<Voter> Voters { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}