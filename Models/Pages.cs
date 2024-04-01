using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Educational_platform.Models
{
    public class Pages
    {
        [Key]
        public int IdPage { get; set; }

        [Required]
        [ForeignKey("Course")]
        public int IdCourse { get; set; }

        [Required]
        public string Path { get; set; }

        public virtual Courses Course { get; set; }
    }
}
