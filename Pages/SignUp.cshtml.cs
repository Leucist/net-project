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
        // private readonly PasswordHasher<Users> _passwordHasher;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public SignUp(UsersContext context, UserManager<Users> userManager, RoleManager<Role> roleManager)
        {
            // _logger = logger;
            _context = context;
            // _passwordHasher = new PasswordHasher<Users>();
            _userManager = userManager;
            _roleManager = roleManager;
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
                .Where(u => u.UserName == Username || u.Email == Email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                // User with such username or email already exists
                ModelState.AddModelError(string.Empty, "A user with the same username or email already exists.");
                return Page();
            }

            // // Initialise student role
            // Role studentRole = new Role
            // {
            //     Name = "student"
            // };

            // Create new user
            var newUser = new Users
            {
                Name = Name,
                Surname = Surname,
                Email = Email,
                UserName = Username,
                // Role = studentRole
            };

            // // Hash the password
            // newUser.PasswordHash = _passwordHasher.HashPassword(newUser, Password);

            // Adds new user
            var result = await _userManager.CreateAsync(newUser, Password);

            // Redirect to the SignIn
            // return RedirectToAction("SignIn", "Account");

            if (result.Succeeded) {
                await _context.SaveChangesAsync();
                // Adds student role to the User
                await _userManager.AddToRoleAsync(newUser, "student");
                // Redirect to a success page or display a success message
                return RedirectToPage("./SignIn");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }

        public void OnGet()
        {
        }
    }
}