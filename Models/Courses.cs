using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Educational_platform.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [MaxLength(20000)]
        public string Description { get; set; }

        public virtual ICollection<Enrollments> Enrollments { get; set; }
        public virtual ICollection<Pages> Pages { get; set; } 
    }
}
