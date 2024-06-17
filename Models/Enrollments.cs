using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Models
{
    [PrimaryKey(nameof(CourseId), nameof(UserId))]
    public class Enrollments
    {
        public int CourseId { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual Courses Course { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; }
    }
}
