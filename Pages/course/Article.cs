using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_platform.Pages.course
{
    public class Article : PageContent {
        public string? Header { get; set; }
        public string? Text { get; set; }

        public Article()
        {
            Type = ContentType.Article;
        }

        public Article(string? header, string? text)
        {
            Type = ContentType.Article;
            Header = header;
            Text = text;
        }
    }
}