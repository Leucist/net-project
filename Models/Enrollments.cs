using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Educational_platform.Models
{
    public class Enrollments
    {
        [Key, Column(Order = 0)]
        public int IdCourses { get; set; }
        [Key, Column(Order = 1)]
        public int IdUsers { get; set; }

        [ForeignKey("IdCourses")]
        public virtual Courses Course { get; set; }

        [ForeignKey("IdUsers")]
        public virtual Users User { get; set; }
    }
}
