using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVoting.Data.BoundedContexts.ResultsProcessing
{
    /// <summary>
    /// Vote record for results processing
    /// Focuses on tallying and analysis operations
    /// </summary>
    [Table("Votes")]
    public class ResultsVote
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime VoteDate { get; set; }
        public int VoterId { get; set; }
        public int CandidateId { get; set; }
        public int PositionId { get; set; }
        
        // Results processing specific properties
        public bool IsValidVote { get; set; } = true;
        public string ValidationNotes { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string ProcessedBy { get; set; }
        
        // Navigation properties for results analysis
        public virtual ResultsCandidate Candidate { get; set; }
        public virtual ResultsPosition Position { get; set; }
        public virtual ResultsVoter Voter { get; set; }
    }
}