using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Models
{
    [PrimaryKey(nameof(IdCourse), nameof(IdUser))]
    public class Enrollments
    {
        public int IdCourse { get; set; }

        public int IdUser { get; set; }

        [ForeignKey("IdCourses")]
        public virtual Courses Course { get; set; }

        [ForeignKey("IdUsers")]
        public virtual Users User { get; set; }
    }
}
