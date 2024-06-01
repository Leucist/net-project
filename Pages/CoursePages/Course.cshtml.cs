using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.Data;
using Educational_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Educational_platform.Pages.CoursePages
{
    public class Course : PageModel
    {
        private readonly UsersContext _context;

        public Course(UsersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Courses? CourseOnPage { get; set; }
        [BindProperty]
        public List<Models.Pages> CoursePages { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CourseOnPage = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (CourseOnPage == null)
            {
                return RedirectToPage("../Error");
            }

            CoursePages = await _context.Pages
                .Where(p => p.IdCourse == id)
                .OrderBy(p => p.IdPage)
                .ToListAsync();


            return Page();
        }
    }
}