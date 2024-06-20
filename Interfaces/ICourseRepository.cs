using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.Models;
using Educational_platform.ViewModels;

namespace Educational_platform.Interfaces
{
    public interface ICourseRepository
    {
        Task<Courses?> GetCourseAsync (int courseId);
        Task<List<Courses>> GetTopCoursesAsync (int amount);
        Task<List<Courses>> FindCoursesAsync (string keyword);
    }
}