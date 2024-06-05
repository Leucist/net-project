using System;
using System.Collections.Generic;
using System.Linq;
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
        public Models.Pages? PageContent { get; set; }

        [BindProperty]
        public List<Article>? ArticlesOnPage { get; set; }

        public async Task<IActionResult> OnGetAsync(int courseId, int pageId)
        {
            PageContent = await _context.Pages
                .FirstOrDefaultAsync(p => p.IdPage == pageId);

            if (PageContent == null)
            {
                return RedirectToPage("../Error");
            }

            return Page();
        }
    }
}