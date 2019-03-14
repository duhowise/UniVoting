using System;

namespace UniVoting.Model
{
    /// <summary>
    /// A class which represents the Vote table.
    /// </summary>
    public partial class Vote
    {
        public virtual int Id { get; set; }
        public virtual int? VoterId { get; set; }
        public virtual int? CandidateId { get; set; }
        public virtual int? PositionId { get; set; }
        public virtual DateTime? Time { get; set; }
        public  Voter Voter { get; set; }
        public  Candidate Candidate { get; set; }
        public  Position Position { get; set; }
    }
}