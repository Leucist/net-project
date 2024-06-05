using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Educational_platform.Pages.course
{
    public abstract class PageContent {
        [JsonConverter(typeof(JsonStringEnumConverter))]    /* instruction to serialise Type as a string */
        public ContentType Type { get; set; }

        [JsonConstructor]
        public PageContent() {}
    }
}