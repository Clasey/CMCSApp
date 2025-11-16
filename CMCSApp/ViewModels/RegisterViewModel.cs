using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMCSApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string DisplayName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a role.")]
        public string SelectedRole { get; set; } = string.Empty;

        // Convert roles to SelectListItem
        public IEnumerable<SelectListItem> AvailableRoles { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Text = "Lecturer", Value = "Lecturer" },
            new SelectListItem { Text = "Coordinator", Value = "Coordinator" },
            new SelectListItem { Text = "Manager", Value = "Manager" }
        };
    }
}
