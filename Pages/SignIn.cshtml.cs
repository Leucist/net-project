using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Educational_platform.Data;
using Microsoft.AspNetCore.Identity;
using Educational_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Pages
{
    public class SignIn : PageModel
    {
        
        private readonly UsersContext _context;
        private readonly PasswordHasher<Users> _passwordHasher;

        public SignIn(UsersContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Users>();
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

            // Check if the user with the given username exists
            var user = await _context.Users
                .Where(u => u.Username == Username)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                // User with the given username not found
                ModelState.AddModelError(string.Empty, "No user with the given username was found.");
                return Page();
            }

            // Check the hashed password
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, Password);

            if (result == PasswordVerificationResult.Failed)
            {
                // Password incorrect
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }


            // return RedirectToAction("Index", "Home");

            // Redirect to a success page or display a success message
            return RedirectToPage("./Index");
        }

        public void OnGet()
        {
        }
    }
}