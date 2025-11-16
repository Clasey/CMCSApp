using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMCSApp.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty; // store plain text for demo; hash in production

        [Required]
        public string Role { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Claim>? Claims { get; set; }
    }
}
