using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMCSApp.Models
{
    public enum ClaimStatus
    {
        Pending,
        Verified,
        Approved,
        Rejected,
        SentBack
    }

    public class Claim
    {
        [Key]
        public string ClaimId { get; set; } = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

        [Required]
        public string LecturerId { get; set; } = string.Empty;

        [Required]
        public string Month { get; set; } = string.Empty;

        [Required]
        public int HoursWorked { get; set; }

        [Required]
        public decimal HourlyRate { get; set; }

        public string? Notes { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Pending;

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public string? DocumentPath { get; set; }

        public string? RejectionReason { get; set; }

        // optional foreign key to User
        [ForeignKey("LecturerId")]
        public User? User { get; set; }
    }
}
