using System.Collections.Generic;

namespace UniVoting.Model
{
    /// <summary>
    /// A class which represents the UserType table.
    /// </summary>
    public partial class UserType
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IEnumerable<User> Users { get; set; }
    }
}