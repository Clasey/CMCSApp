using System.ComponentModel.DataAnnotations;

namespace CMCSApp.ViewModels
{
    public class ClaimViewModel
    {
        [Required] public string LecturerId { get; set; } = "";
        [Required] public string Month { get; set; } = "";
        [Required] public int HoursWorked { get; set; }
        [Required] public decimal HourlyRate { get; set; }
        public string? Notes { get; set; }
    }
}
