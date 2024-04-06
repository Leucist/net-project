using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Educational_platform.Data;
using Educational_platform.Models;

namespace Educational_platform.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsersContext _context;

        public AccountController(UsersContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string name, string surname, string email, string username, string password)
        {
            // Check if the user with the given username or email already exists
            var existingUser = await _context.Users
                .Where(u => u.Username == username || u.Email == email)
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                // User with such username or email already exists
                ModelState.AddModelError(string.Empty, "A user with the same username or email already exists.");
                return View();
            }

            // Create new user
            var newUser = new Users
            {
                Name = name,
                Surname = surname,
                Email = email,
                Username = username,
                Password = password 
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Redirect to the SignIn
            return RedirectToAction("SignIn", "Account");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}