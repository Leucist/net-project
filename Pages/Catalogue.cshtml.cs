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

        private int shownCoursesAmount = 10;

        [BindProperty]
        public HashSet<Courses> CoursesOnPage { get; set; }     /* HashSet to avoid duplicates in search */
        [BindProperty]
        public string KeywordsInput { get; set; }

        public CatalogueModel(UsersContext context)
        {
            _context = context;
            CoursesOnPage = new HashSet<Courses>();
        }

        public async Task<IActionResult> OnPostAsync() {
            // - Search yet to be improved by forming the top based on the amount of the matching keywords in Name/Desc. - (c) leucist
            if (KeywordsInput is null) {
                return Page();
            }
            string[] keywords = KeywordsInput.Split(' ');
            // Using async search for each keyword
            var tasks = keywords.Select(async keyword => {
                // Retrieves 10 courses, where each matches at least one keyword from the KeywordsInput 
                var result = await _context.Courses
                    .Where(c => c.Name.Contains(keyword) || c.Description.Contains(keyword))
                    // .OrderBy(c => c.Enrollments.Count) - may be used to form the top of some sort - (c) leucist
                    .Take(shownCoursesAmount)
                    .ToListAsync();

                lock(CoursesOnPage) {
                    foreach(var course in result) {
                        // if there are still less than 'shownCoursesAmount' number of courses found
                        if (!(CoursesOnPage.Count >= shownCoursesAmount))
                        CoursesOnPage.Add(course);
                    }
                }
            });
            // Waits until all of the threads are completed
            await Task.WhenAll(tasks);

            return Page();
        }

        public async Task OnGetAsync()
        {
            // Buffer variable to store newly retrieved courses before their placement in HashSet
            List<Courses> coursesOnPage;
            // Retrieves the first 'shownCoursesAmount' number of courses from the database
            coursesOnPage = await _context.Courses
                .OrderBy(c => c.Id)
                .Take(shownCoursesAmount)
                .ToListAsync();
            foreach (var course in coursesOnPage) {
                CoursesOnPage.Add(course);
            }
        }
    }
}
