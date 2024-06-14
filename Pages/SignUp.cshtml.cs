using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Educational_platform.Data;
using Educational_platform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Pages
{
    public class SignUp : PageModel
    {
        // private readonly ILogger<SignUp> _logger;
        private readonly UsersContext _context;
        private readonly PasswordHasher<Users> _passwordHasher;

        public SignUp(UsersContext context)
        {
            // _logger = logger;
            _context = context;
            _passwordHasher = new PasswordHasher<Users>();
        }

        // [BindProperty]
        // public Educational_platform.Models.SignUpModel SignUpModel { get; set; }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        [BindProperty]
        [Required]
        public string Surname { get; set; }

        [BindProperty]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate the input data
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if the user with the given username or email already exists
            var existingUser = await _context.Users
                .Where(u => u.Username == Username || u.Email == Email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                // User with such username or email already exists
                ModelState.AddModelError(string.Empty, "A user with the same username or email already exists.");
                return Page();
            }

            // finds the greatest id among users and makes new user's id greater than this by one 
            int newUserId = await _context.Users
                .OrderByDescending(u => u.Id)
                .Select(u => u.Id)
                .FirstOrDefaultAsync() + 1;
            
            // gets the student role id from the database
            int studentRole = await _context.Roles
                .Where(r => r.Name.ToLower() == "student")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            // Create new user
            var newUser = new Users
            {
                Id = newUserId,
                Name = Name,
                Surname = Surname,
                Email = Email,
                Username = Username,
                Role = studentRole
            };
            // Hash the password
            newUser.Password = _passwordHasher.HashPassword(newUser, Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Redirect to the SignIn
            // return RedirectToAction("SignIn", "Account");

            // Redirect to a success page or display a success message
            return RedirectToPage("./SignIn");
        }

        public void OnGet()
        {
        }
    }
}