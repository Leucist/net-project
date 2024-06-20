using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.ViewModels;

namespace Educational_platform.Interfaces
{
    public interface ICourseService
    {
        Task<CourseInfo?> GetCourseInfoAsync(int courseId);
        Task<CourseList> GetTopCoursesAsync(int amount);
        Task<CourseList> FindCoursesAsync(string keyword);
    }
}