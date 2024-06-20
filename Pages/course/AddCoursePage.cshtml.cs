using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Educational_platform.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Educational_platform.Pages.course
{
    [Authorize(Roles = "admin")]
    public class AddCoursePage : PageModel
    {
        private readonly UsersContext _context;

        private Models.Pages _newPage;

        [BindProperty]
        [Required]
        public Article NewPageContent { get; set; }

        public AddCoursePage(UsersContext context)
        {
            _context = context;
            _newPage = new Models.Pages();
            NewPageContent = new Article();
        }
        
        [BindProperty]
        [Required]
        public string NewPageName { get; set; }

        [BindProperty]
        [Required]
        public string NewPageCourseName { get; set; }

        // [BindProperty]
        // [Required]
        // public string NewPageContent { get; set; }

        public async Task<IActionResult> OnPostAsync() {
            // Validate the input data
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the course with the given name exists
            var existingCourse = await _context.Courses
                .Where(c => c.Name == NewPageCourseName)
                .FirstOrDefaultAsync();
            
            if (existingCourse is null) {
                ModelState.AddModelError(string.Empty, "No course with the given name found.");
                return Page();
            }

            // Check if the page with the given name exists in this course
            var existingPage = await _context.Pages
                .Where(p => p.IdCourse == existingCourse.Id && p.PageName == NewPageName)
                .FirstOrDefaultAsync();
            
            if (existingPage is not null) {
                ModelState.AddModelError(string.Empty, "Page with the given name already exists in the course.");
                return Page();
            }

            // finds the id of the last page in the course and makes new page id greater by one 
            int newPageId = await _context.Pages
                .Where(p => p.IdCourse == existingCourse.Id)
                .OrderByDescending(p => p.IdPage)
                .Select(p => p.IdPage)
                .FirstOrDefaultAsync() + 1;

            // builds new page content file path
            string newPageContentPath = "Pages/course/contents/page_" + existingCourse.Id + "_" + newPageId + ".json";

            // Serialising Article to the JSON "PageContent List"
            // envelops Article -> List<PageContent>
            List<PageContent> pageContent = new List<PageContent>();
            pageContent.Add(NewPageContent);
            // inits serialising options
            JsonSerializerOptions options = new JsonSerializerOptions { Converters = { new PageContentConverter() } };
            string jsonString = JsonSerializer.Serialize<List<PageContent>>(pageContent, options);
            // dumps JSON to the file
            System.IO.File.WriteAllText(newPageContentPath, jsonString);

            // Filling the new Pages row for the database
            _newPage.IdCourse = existingCourse.Id;
            _newPage.IdPage = newPageId;
            _newPage.Path = newPageContentPath;
            _newPage.PageName = NewPageName;

            _context.Pages.Add(_newPage);
            await _context.SaveChangesAsync();

            // Redirect to the newly added page
            return RedirectToPage("/course/CoursePage", new { courseId = existingCourse.Id, pageId = newPageId });
        }

        public void OnGet()
        {
        }
    }
}