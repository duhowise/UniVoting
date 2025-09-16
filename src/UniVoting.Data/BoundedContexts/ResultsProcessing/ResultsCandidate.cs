using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVoting.Data.BoundedContexts.ResultsProcessing
{
    /// <summary>
    /// Results summary view for candidates
    /// Optimized for results calculation and reporting
    /// </summary>
    [Table("Candidates")]
    public class ResultsCandidate
    {
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int PositionId { get; set; }
        
        // Navigation properties
        public virtual ResultsPosition Position { get; set; }
        public virtual ICollection<ResultsVote> Votes { get; set; } = new List<ResultsVote>();
        
        // Computed properties for results
        public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
        public int VoteCount => Votes?.Count(v => v.IsValidVote) ?? 0;
        public decimal VotePercentage { get; set; }  // Calculated during results processing
        public int Ranking { get; set; }  // Calculated during results processing
        public bool IsWinner { get; set; }  // Determined during results processing
    }
}