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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace Educational_platform.Pages
{
    public class SignIn : PageModel
    {
        
        private readonly UsersContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public SignIn(UsersContext context, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

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

            // Get user by username
            var user = await _userManager.FindByNameAsync(Username);
            // Check if the user with the given username exists
            if (user == null)
            {
                // User with the given username not found
                ModelState.AddModelError(string.Empty, "No user with the given username was found.");
                return Page();
            }

            

            // Try to log in
            var result = await _signInManager.PasswordSignInAsync(user, Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToPage("./Index");
            }

            // Password incorrect
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return Page();

            // // Add role
            // await _userManager.AddToRoleAsync(user, user.Role.Name);
            // var claims = new List<Claim>
            // {
            //     new Claim(ClaimTypes.Name, user.UserName),
            //     new Claim(ClaimTypes.Role, user.Role.Name)
            // };

            // var identity = new ClaimsIdentity(claims, "login");
            // ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // await HttpContext.SignInAsync(principal);
        }

        public void OnGet()
        {
        }
    }
}