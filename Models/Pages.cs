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
        [MaxLength(400)]
        public string Path { get; set; }

        [Required]
        [MaxLength(300)]
        public string PageName { get; set; }

        public virtual Courses Course { get; set; }
    }
}
