using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_platform.Pages.course
{
    public class Test : PageContent {
        public List<Question> Questions { get; set; }

        public Test()
        {
            Type = ContentType.Test;
            Questions = new List<Question>();
        }

        public Test(List<Question> questions)
        {
            Type = ContentType.Test;
            Questions = questions;
        }

        public void addQuestion(Question question) {
            Questions.Add(question);
        }
    }
}