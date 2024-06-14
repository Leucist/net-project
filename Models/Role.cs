using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Educational_platform.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(7)]
        public string Name { get; set; }
    }
}
