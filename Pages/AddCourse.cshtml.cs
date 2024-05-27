using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.Data;
using Educational_platform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Pages
{
    public class AddCourse : PageModel
    {
        private readonly UsersContext _context;

        [BindProperty]
        [Required]
        public string CourseTitle { get; set; }
        [BindProperty]
        [Required]
        public string CourseDescription { get; set; }

        public AddCourse(UsersContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync() {
            var existingCourse = await _context.Courses
                .Where(c => c.Title == CourseTitle)
                .FirstOrDefaultAsync();
            if (existingCourse != null) {
                // Course with such title already exists
                ModelState.AddModelError(string.Empty, "A course with the same title already exists.");
                return Page();
            }

            // - Description and title may be checked - (c) leucist

            var newCourse = new Courses {
                Title = CourseTitle,
                Description = CourseDescription,
            };

            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Catalogue");
        }

        public void OnGet() {}
    }
}