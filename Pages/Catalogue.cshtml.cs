using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Educational_platform.Models;
using Educational_platform.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Pages
{
    public class CatalogueModel : PageModel
    {
        private readonly UsersContext _context;

        public CatalogueModel(UsersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Courses> CoursesOnPage { get; set; }

        // public void OnGet() { }
        public async Task OnGetAsync()
        {
            // Retrieve the first 10 courses from the database
            CoursesOnPage = await _context.Courses
                .OrderBy(c => c.Id)
                .Take(10)
                .ToListAsync();
        }
    }
}
