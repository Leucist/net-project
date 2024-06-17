using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Educational_platform.Models
{
    public class Users : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(100)]
        public override string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public override string UserName { get; set; }

        // [Required]
        // [MaxLength(100)]
        // public string Password { get; set; }

        public virtual ICollection<Enrollments> Enrollments { get; set; }

        public virtual Role Role { get; set; }
    }
}
