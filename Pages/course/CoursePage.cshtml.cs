using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Educational_platform.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Educational_platform.Pages.course
{
    public class CoursePage : PageModel
    {
        private readonly UsersContext _context;

        public CoursePage(UsersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Pages? CurrentPage { get; set; }

        [BindProperty]
        public List<PageContent>? PageContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int courseId, int pageId)
        {
            // Loading current Page from the database
            CurrentPage = await _context.Pages
                .FirstOrDefaultAsync(p => p.IdPage == pageId);
            // if page wasn't found
            if (CurrentPage == null)
            {
                return RedirectToPage("../Error");
            }

            // Loading page content from the JSON file
            string jsonString = System.IO.File.ReadAllText(CurrentPage.Path);
            // Deserialising JSON to the PageContent List
            JsonSerializerOptions options = new JsonSerializerOptions { Converters = { new PageContentConverter() } };
            PageContent = JsonSerializer.Deserialize<List<PageContent>>(jsonString, options);

            return Page();
        }
    }
}