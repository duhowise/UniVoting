using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVoting.Data.BoundedContexts.ElectionManagement
{
    /// <summary>
    /// Streamlined candidate model for election management operations
    /// Focuses on registration, approval, and management aspects
    /// </summary>
    [Table("Candidates")]
    public class ElectionCandidate
    {
        [Key]
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Biography { get; set; }
        public string PhotoPath { get; set; }
        
        // Election management specific properties
        public DateTime RegistrationDate { get; set; }
        public string RegistrationStatus { get; set; }  // Pending, Approved, Rejected
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectionReason { get; set; }
        
        // Foreign keys
        public int PositionId { get; set; }
        public int? RankId { get; set; }
        
        // Navigation properties
        public virtual ElectionPosition Position { get; set; }
        
        // Computed properties for management
        public string FullName => $"{FirstName} {MiddleName} {LastName}".Trim();
        public bool IsApproved => RegistrationStatus == "Approved";
    }
}