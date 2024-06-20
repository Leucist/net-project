using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.Pages.course;

namespace Educational_platform.ViewModels
{
    public class NewPageContent
    {
        public string? PageName { get; set; }
        public string? CourseName { get; set; }
        public List<Article> TextSections { get; set; }
        public Video? Video { get; set; }
        public Test? Test { get; set; }

        public NewPageContent() {
            TextSections = new List<Article>();
            for (int i = 0; i < 3; i++) {
                TextSections.Add(new Article());
            }
        }
    }
}