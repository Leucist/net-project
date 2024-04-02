using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Educational_platform.Pages
{
    public class SignIn : PageModel
    {
        private readonly ILogger<SignIn> _logger;

        public SignIn(ILogger<SignIn> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        [Required]
        public string? Username { get; set; }

        [BindProperty]
        [Required]
        public string? Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate the input data
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // *Creates a new user account* await may be used.

            // Redirect to a success page or display a success message
            return RedirectToPage("./Index");
        }

        public void OnGet()
        {
        }
    }
}