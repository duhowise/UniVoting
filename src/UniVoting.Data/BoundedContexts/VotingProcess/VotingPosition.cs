using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVoting.Data.BoundedContexts.VotingProcess
{
    /// <summary>
    /// Read-only position view for voting process
    /// Contains only information needed during voting
    /// </summary>
    [Table("Positions")]
    public class VotingPosition
    {
        [Key]
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxSelections { get; set; }
        public bool IsActive { get; set; }
        
        // Voting process specific properties
        public int DisplayOrder { get; set; }
        public string VotingInstructions { get; set; }
        
        // Navigation properties (read-only for voting)
        public virtual ICollection<VotingCandidate> Candidates { get; set; } = new List<VotingCandidate>();
        
        // Computed properties for voting UI
        public bool AllowsMultipleSelection => MaxSelections > 1;
        public string SelectionText => MaxSelections == 1 ? "Select one" : $"Select up to {MaxSelections}";
    }
}