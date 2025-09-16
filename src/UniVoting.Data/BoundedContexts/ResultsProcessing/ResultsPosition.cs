using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace UniVoting.Data.BoundedContexts.ResultsProcessing
{
    /// <summary>
    /// Position summary for results processing
    /// Contains aggregated voting statistics
    /// </summary>
    [Table("Positions")]
    public class ResultsPosition
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }
        public int MaxSelections { get; set; }
        
        // Navigation properties
        public virtual ICollection<ResultsCandidate> Candidates { get; set; } = new List<ResultsCandidate>();
        public virtual ICollection<ResultsVote> Votes { get; set; } = new List<ResultsVote>();
        
        // Computed properties for results analysis
        public int TotalValidVotes => Votes?.Count(v => v.IsValidVote) ?? 0;
        public int TotalVotersParticipated => Votes?.Select(v => v.VoterId).Distinct().Count() ?? 0;
        public decimal ParticipationRate { get; set; }  // Calculated during results processing
        public bool ResultsFinalized { get; set; }
        public DateTime? ResultsPublishedDate { get; set; }
        
        // Results summary
        public string WinnerSummary { get; set; }  // e.g., "John Doe (45.2%)"
        public bool HasTie { get; set; }
        public string TieBreakingMethod { get; set; }
    }
}