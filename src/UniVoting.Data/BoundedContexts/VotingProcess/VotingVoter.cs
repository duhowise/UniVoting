using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVoting.Data.BoundedContexts.VotingProcess
{
    /// <summary>
    /// Streamlined voter model for voting process operations
    /// Focuses on authentication and voting status tracking
    /// </summary>
    [Table("Voters")]
    public class VotingVoter
    {
        [Key]
        public int Id { get; set; }
        
        public string VoterCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        // Voting process specific properties
        public bool HasVoted { get; set; }
        public DateTime? VotingStartTime { get; set; }
        public DateTime? VotingCompletedTime { get; set; }
        public string VotingStatus { get; set; }  // NotStarted, InProgress, Completed, Interrupted
        public string AuthenticationMethod { get; set; }
        public DateTime? LastAuthenticationTime { get; set; }
        
        // Computed properties for voting process
        public string FullName => $"{FirstName} {LastName}".Trim();
        public bool CanVote => !HasVoted && VotingStatus != "Completed";
        public TimeSpan? VotingDuration => VotingCompletedTime - VotingStartTime;
    }
}