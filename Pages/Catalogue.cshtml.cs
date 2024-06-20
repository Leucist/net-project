using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Educational_platform.Models;
using Educational_platform.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Educational_platform.ViewModels;
using Educational_platform.Services;
using Educational_platform.Interfaces;

namespace Educational_platform.Pages
{
    public class CatalogueModel : PageModel
    {
        private readonly ICourseService _courseService;

        private int shownCoursesAmount = 10;

        [BindProperty]
        public CourseList CoursesOnPage { get; set; }
        [BindProperty]
        public string? KeywordsInput { get; set; }

        public CatalogueModel(ICourseService courseService)
        {
            _courseService = courseService;
            CoursesOnPage = new CourseList();
        }

        public async Task<IActionResult> OnPostAsync() {
            // - Search yet to be improved by forming the top based on the amount of the matching keywords in Name/Desc. - (c) leucist
            if (KeywordsInput is null) {
                await LoadDefaultCoursesAsync();
                return Page();
            }
            string[] keywords = KeywordsInput.Trim().Split(' ');

            HashSet<CourseInfo> coursesFound = new HashSet<CourseInfo>();

            // Using async search for each keyword
            var tasks = keywords.Select(async keyword => {
                // Retrieves courses which name or description matches at least one keyword from the KeywordsInput 
                var result = await _courseService.FindCoursesAsync(keyword);
                // foreach (var course in result.Courses) {
                //     coursesFound.Add(course);
                // }
                bool alreadyExists = false;
                foreach (var newCourse in result.Courses) {
                    foreach (var course in coursesFound) {
                        if (course.Name == newCourse.Name){
                            alreadyExists = true;
                        }
                    }
                    if (!alreadyExists) {
                        coursesFound.Add(newCourse);
                    }
                }
            });
            // Waits until all of the threads are completed
            await Task.WhenAll(tasks);

            CoursesOnPage.Courses = coursesFound.ToList();
            CoursesOnPage.Count = coursesFound.Count;

            return Page();
        }

        public async Task OnGetAsync()
        {
            await LoadDefaultCoursesAsync();
        }

        public async Task LoadDefaultCoursesAsync() {
            CourseList CoursesOnPage = await _courseService.GetTopCoursesAsync(shownCoursesAmount);
        }
    }
}
