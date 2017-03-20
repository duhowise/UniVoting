namespace UniVoting.Model
{
    /// <summary>
    /// A class which represents the User table.
    /// </summary>
    public partial class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual int? TypeId { get; set; }
        public virtual UserType UserType { get; set; }
    }
}