using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Educational_platform.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public virtual ICollection<Enrollments> Enrollments { get; set; }
    }
}
