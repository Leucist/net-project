using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.Interfaces;
using Educational_platform.Models;
using Educational_platform.ViewModels;

namespace Educational_platform.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;
        public CourseService(ICourseRepository courseRepo) {
            _courseRepo = courseRepo;
        }

        private CourseInfo ExtractCourseInfo(Courses course) {
            CourseInfo courseInfo = new CourseInfo
            (
                course.Id,
                course.Name,
                course.Description
            );
            return courseInfo;
        }

        public async Task<CourseInfo?> GetCourseInfoAsync(int courseId) {
            var course = await _courseRepo.GetCourseAsync(courseId);
            if (course == null) {
                return null;
            }
            return ExtractCourseInfo(course);
        }

        public async Task<CourseList> GetTopCoursesAsync(int amount) {
            List<Courses> topCourses = await _courseRepo.GetTopCoursesAsync(amount);
            CourseList topCoursesInfo = new CourseList();
            foreach (Courses topCourse in topCourses) {
                CourseInfo courseInfo = ExtractCourseInfo(topCourse);
                topCoursesInfo.Courses.Add(courseInfo);
                topCoursesInfo.Count++;
            }
            return topCoursesInfo;
        }

        public async Task<CourseList> FindCoursesAsync(string keyword) {
            List<Courses> foundCourses = await _courseRepo.FindCoursesAsync(keyword);
            CourseList foundCoursesList = new CourseList();
            foreach (var course in foundCourses) {
                foundCoursesList.Courses.Add(ExtractCourseInfo(course));
                foundCoursesList.Count++;
            }
            return foundCoursesList;
        }
    }
}