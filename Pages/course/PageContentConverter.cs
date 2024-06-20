using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Educational_platform.Pages.course
{
    public class PageContentConverter : JsonConverter<List<PageContent>> {
        public override List<PageContent> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // initialises list of PageContent objects for the future filling
            var list = new List<PageContent>();
            
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            // ^ "using" for correct recourse freeing after execution
            {
                // iterates through each of the objects stored in the provided json 
                foreach (var element in doc.RootElement.EnumerateArray()) {
                    var type = element.GetProperty("Type").GetString();
                    // defines different handling depending on the content type
                    PageContent content = type switch
                    {
                        "Article" => JsonSerializer.Deserialize<Article>(element.GetRawText(), options) 
                                 ?? throw new JsonException("Deserialization of Article returned null"),
                        "Video" => JsonSerializer.Deserialize<Video>(element.GetRawText(), options) 
                                ?? throw new JsonException("Deserialization of Video returned null"),
                        "Test" => JsonSerializer.Deserialize<Test>(element.GetRawText(), options) 
                                ?? throw new JsonException("Deserialization of Test returned null"),
                        _ => throw new JsonException($"Unknown type '{type}'"),
                    };

                    list.Add(content);
                }
            }
            return list;
        }

        public override void Write(Utf8JsonWriter writer, List<PageContent> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var item in value)
            {
                if (item is Article article)
                {
                    JsonSerializer.Serialize(writer, article, options);
                }
                else if (item is Video video)
                {
                    JsonSerializer.Serialize(writer, video, options);
                }
            }

            writer.WriteEndArray();
        }
    }
}