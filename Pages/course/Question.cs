using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_platform.Pages.course
{
    public struct Question {
        public string question;
        public Dictionary<string, int> answers;

        public Question(string question, Dictionary<string, int> answers) {
            this.question = question;
            this.answers = answers;
        }
    }
}