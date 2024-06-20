using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Educational_platform.Pages
{
    public class Error403 : PageModel
    {
        private readonly ILogger<Error403> _logger;

        public Error403(ILogger<Error403> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}