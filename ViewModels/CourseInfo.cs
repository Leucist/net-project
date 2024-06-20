using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_platform.ViewModels
{
    public class CourseInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CourseInfo(int courseId, string courseName, string courseDescription) {
            Id = courseId;
            Name = courseName;
            Description = courseDescription;
        }
    }
}