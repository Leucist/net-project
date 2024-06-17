using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Educational_platform.Models
{
    public class Role : IdentityRole
    {
        public virtual ICollection<Users> Users { get; set; }   /* navigation property */
    }
}
