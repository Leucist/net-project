using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.Data;
using Educational_platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Pages
{
    [Authorize(Roles = "admin")]
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
                .Where(c => c.Name == CourseTitle)
                .FirstOrDefaultAsync();
            if (existingCourse != null) {
                // Course with such title already exists
                ModelState.AddModelError(string.Empty, "A course with the same title already exists.");
                return Page();
            }

            // - Description and title may be checked - (c) leucist

            // finds the greatest id among courses and makes new course's id greater than this by one 
            int newCourseId = await _context.Courses
                .OrderByDescending(c => c.Id)
                .Select(c => c.Id)
                .FirstOrDefaultAsync() + 1;

            var newCourse = new Courses {
                Id = newCourseId,
                Name = CourseTitle,
                Description = CourseDescription,
            };

            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Catalogue");
        }

        public void OnGet() {}
    }
}