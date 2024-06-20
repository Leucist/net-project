using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.Data;
using Educational_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Repositories
{
    public class CourseRepository : Interfaces.ICourseRepository
    {
        private readonly UsersContext _context;
        private readonly IServiceScopeFactory _scopeFactory;
        public CourseRepository(UsersContext context, IServiceScopeFactory scopeFactory) {
            _context = context;
            _scopeFactory = scopeFactory;
        }

        public async Task<Courses?> GetCourseAsync (int courseId) {
            return await _context.Courses.FindAsync (courseId);
        }

        public async Task<List<Courses>> GetTopCoursesAsync (int amount) {
            return await _context.Courses
                .OrderBy(c => c.Enrollments.Count)
                .Take(amount)
                .ToListAsync();
        }

        public async Task<List<Courses>> FindCoursesAsync (string keyword) {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<UsersContext>();
                return await context.Courses
                    .Where(c => c.Name.Contains(keyword) || c.Description.Contains(keyword))
                    .ToListAsync();
            }
        }
    }
}