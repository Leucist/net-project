using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Educational_platform.Models
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Enrollments> Enrollments { get; set; }
        public virtual ICollection<Pages> Pages { get; set; } 
    }
}
