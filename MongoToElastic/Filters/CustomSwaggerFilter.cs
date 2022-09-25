using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MongoToElastic.Filters
{
    public class CustomSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            List<KeyValuePair<string, OpenApiPathItem>> abcd = swaggerDoc.Paths.Where(x => x.Key.ToLower() == "/restart/{appid}/{magicid}" || x.Key.ToLower() == "/reload" || x.Key.ToLower() == "/").ToList();
            abcd.ForEach(x =>
            {
                swaggerDoc.Paths.Remove(x.Key);
            });
        }
    }
}
