using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVoting.Data.BoundedContexts.ElectionManagement
{
    /// <summary>
    /// Streamlined position model for election management operations
    /// Focuses on setup and configuration aspects
    /// </summary>
    [Table("Positions")]
    public class ElectionPosition
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxSelections { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        // Election management specific properties
        public string CreatedBy { get; set; }
        public string ApprovalStatus { get; set; }
        public string RequiredRankLevel { get; set; }
        
        // Navigation for candidates in this context
        public virtual ICollection<ElectionCandidate> Candidates { get; set; } = new List<ElectionCandidate>();
    }
}