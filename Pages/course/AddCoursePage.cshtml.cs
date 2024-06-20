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
        public ViewModels.NewPageContent NewPageContent { get; set; }

        public AddCoursePage(UsersContext context)
        {
            _context = context;
            _newPage = new Models.Pages();
            NewPageContent = new ViewModels.NewPageContent();
        }

        // [BindProperty]
        // [Required]
        // public string NewPageContent { get; set; }


        // public IActionResult OnPostAddTextSection() {
        //     if (!ModelState.IsValid) { return Page(); }
        //     int lastPageId = NewPageContent.TextSections.Count - 1;
        //     if (lastPageId > -1) {
        //         if (NewPageContent.TextSections[lastPageId].Header is null ||
        //             NewPageContent.TextSections[lastPageId].Text is null) {
        //                 return Page();
        //             }
        //     }
        //     NewPageContent.TextSections.Add(new Article());
        //     return Page();
        // }


        public async Task<IActionResult> OnPostAsync(IFormFile videoFile) {
            // Validate the input data
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the course with the given name exists
            var existingCourse = await _context.Courses
                .Where(c => c.Name == NewPageContent.CourseName)
                .FirstOrDefaultAsync();
            
            if (existingCourse is null) {
                ModelState.AddModelError(string.Empty, "No course with the given name found.");
                return Page();
            }

            // Check if the page with the given name exists in this course
            var existingPage = await _context.Pages
                .Where(p => p.IdCourse == existingCourse.Id && p.PageName == NewPageContent.PageName)
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

            
            // - Saving the videofile -
            if (videoFile != null && videoFile.Length > 0)
            {
                // var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                // if (!Directory.Exists(uploadsFolder))
                // {
                //     Directory.CreateDirectory(uploadsFolder);
                // }
                // var filePath = Path.Combine(uploadsFolder, videoFile.FileName);

                string videoPath = "Pages/course/contents/videos/" + existingCourse.Id + "_" + newPageId + "_" + videoFile.FileName;
                using (var stream = new FileStream(videoPath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                NewPageContent.Video = new Video(videoPath);
            }

            // builds new page content file path
            string newPageContentPath = "Pages/course/contents/page_" + existingCourse.Id + "_" + newPageId + ".json";



            List<PageContent> pageContent = new List<PageContent>();
            foreach (Article article in NewPageContent.TextSections) {
                pageContent.Add(article);
            }
            if (NewPageContent.Video is not null) {
                pageContent.Add(NewPageContent.Video);
            }

            // // Serialising Article to the JSON "PageContent List"
            // // envelops Article -> List<PageContent>
            // List<PageContent> pageContent = new List<PageContent>();
            // pageContent.Add(NewPageContent);
            // inits serialising options
            JsonSerializerOptions options = new JsonSerializerOptions { Converters = { new PageContentConverter() } };
            string jsonString = JsonSerializer.Serialize<List<PageContent>>(pageContent, options);
            // dumps JSON to the file
            System.IO.File.WriteAllText(newPageContentPath, jsonString);

            // Filling the new Pages row for the database
            _newPage.IdCourse = existingCourse.Id;
            _newPage.IdPage = newPageId;
            _newPage.Path = newPageContentPath;
            _newPage.PageName = NewPageContent.PageName;

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