using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMCSApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        // Selected role chosen by the user
        [Required]
        public string SelectedRole { get; set; } = string.Empty;

        // Dropdown list of roles
        public IEnumerable<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

        public RegisterViewModel() { }
    }
}
