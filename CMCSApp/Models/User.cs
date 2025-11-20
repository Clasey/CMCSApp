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
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public required string FullName { get; set; }

        [Required]
        public required string Role { get; set; }

        // Navigation property for claims
        public ICollection<Claim>? Claims { get; set; }
    }
}
