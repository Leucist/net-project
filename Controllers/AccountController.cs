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
using Microsoft.AspNetCore.Identity;

namespace Educational_platform.Controllers
{
    public class AccountController : Controller
    {
        private readonly UsersContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccountController(UsersContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
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
                // Password = password 
            };
            // Hash the password
            newUser.Password = _passwordHasher.HashPassword(newUser, password);

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

        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            // Check if the user with the given username exists
            var user = await _context.Users
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                // User with the given username not found
                ModelState.AddModelError(string.Empty, "No user with the given username was found.");
                return View();
            }

            // Check the hashed password
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                // Password incorrect
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View();
            }

            // Auth successful

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
    }
}