using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Educational_platform.Models
{
    [PrimaryKey(nameof(IdCourse), nameof(IdPage))]
    public class Pages
    {
        [ForeignKey("Course")]
        public int IdCourse { get; set; }

        public int IdPage { get; set; }

        [Required]
        [MaxLength(400)]
        public string Path { get; set; }

        [Required]
        [MaxLength(300)]
        public string PageName { get; set; }

        public virtual Courses Course { get; set; }
    }
}
