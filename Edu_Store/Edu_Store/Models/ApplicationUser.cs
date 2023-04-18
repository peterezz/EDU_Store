using Edu_Store.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace Edu_Store.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Photo { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Gender Gender { get; set; }
    }
}
