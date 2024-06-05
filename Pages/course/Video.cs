using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Educational_platform.Pages.course
{
    public class Video : PageContent {
        public string Path { get; set; }

        public Video(string path)
        {
            Type = ContentType.Video;
            Path = path;
        }
    }
}