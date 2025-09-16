using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVoting.Data.BoundedContexts.VotingProcess
{
    /// <summary>
    /// Read-only candidate view for voting process
    /// Contains only information needed for voting display
    /// </summary>
    [Table("Candidates")]
    public class VotingCandidate
    {
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Biography { get; set; }
        public string PhotoPath { get; set; }
        
        // Foreign keys
        public int PositionId { get; set; }
        
        // Navigation properties
        public virtual VotingPosition Position { get; set; }
        
        // Computed properties for voting UI
        public string DisplayName => $"{FirstName} {LastName}".Trim();
        public string BallotName => $"{LastName}, {FirstName}".Trim();
        public bool HasPhoto => !string.IsNullOrEmpty(PhotoPath);
    }
}