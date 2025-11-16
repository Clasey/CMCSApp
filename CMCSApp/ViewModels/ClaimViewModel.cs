using System.ComponentModel.DataAnnotations;

namespace CMCSApp.ViewModels
{
    public class ClaimViewModel
    {
        [Required]
        public string Month { get; set; } = string.Empty;

        [Required]
        [Range(1, 744, ErrorMessage = "Hours must be between 1 and 744.")]
        public int HoursWorked { get; set; }

        [Required]
        [Range(0.01, 10000, ErrorMessage = "Hourly rate must be positive.")]
        public decimal HourlyRate { get; set; }

        public string? Notes { get; set; }

        public string LecturerId { get; set; } = string.Empty;
    }
}
