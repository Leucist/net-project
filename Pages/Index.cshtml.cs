using Educational_platform.Interfaces;
using Educational_platform.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Educational_platform.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICourseService _courseService;

        [BindProperty]
        public CourseList CoursesOnPage { get; set; }

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
            CoursesOnPage = new CourseList();
        }

        public async Task<IActionResult> OnGetAsync() 
        {
            CoursesOnPage = await _courseService.GetTopCoursesAsync(5);
            return Page();
        }
    }
}
