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
    public class SignUp : PageModel
    {
        private readonly ILogger<SignUp> _logger;

        public SignUp(ILogger<SignUp> logger)
        {
            _logger = logger;
            // SignUpModel = new Educational_platform.Models.SignUpModel(); // Initialize the SignUpModel instance
        }

        // [BindProperty]
        // public Educational_platform.Models.SignUpModel SignUpModel { get; set; }

        [BindProperty]
        [Required]
        public string? Name { get; set; }

        [BindProperty]
        [Required]
        public string? Surname { get; set; }

        [BindProperty]
        [Required]
        public string? Email { get; set; }

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
            return RedirectToPage("./SignIn");
        }

        public void OnGet()
        {
        }
    }
}