using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace UniVoting.Data.BoundedContexts.ResultsProcessing
{
    /// <summary>
    /// Minimal voter information for results processing
    /// Used for audit trails and vote validation
    /// </summary>
    [Table("Voters")]
    public class ResultsVoter
    {
        [Key]
        public int Id { get; set; }
        
        public string VoterCode { get; set; }
        public DateTime? VotingCompletedTime { get; set; }
        
        // Results processing specific properties
        public bool VoteValidated { get; set; }
        public string ValidationStatus { get; set; }
        public DateTime? ValidationDate { get; set; }
        
        // Navigation properties
        public virtual ICollection<ResultsVote> Votes { get; set; } = new List<ResultsVote>();
        
        // Computed properties for audit
        public int TotalVotesCast => Votes?.Count ?? 0;
        public bool HasValidVotes => Votes?.Any(v => v.IsValidVote) ?? false;
    }
}