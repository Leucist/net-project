using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Educational_platform.Pages.CoursePages
{
    public class CoursePage : PageModel
    {
        private readonly ILogger<CoursePage> _logger;

        public CoursePage(ILogger<CoursePage> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}