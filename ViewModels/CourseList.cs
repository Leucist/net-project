using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_platform.ViewModels
{
    public class CourseList
    {
        public List<CourseInfo> Courses{ get; set; }
        public int Count { get; set; }

        public CourseList() {
            Courses = new List<CourseInfo>();
            Count = 0;
        }
    }
}