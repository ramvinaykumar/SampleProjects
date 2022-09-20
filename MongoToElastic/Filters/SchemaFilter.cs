using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using MongoToElastic.Models.Enums;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MongoToElastic.Filters
{
    public class SchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties == null)
            {
                return;
            }
            if (context.Type.Name == "ENV")
            {
                schema.Default = new OpenApiString(Convert.ToString(Enviornment.G2));
            }
            if (context.Type.Name == "DataMap")
            {
                schema.Default = new OpenApiString(Convert.ToString(Data.All));
            }
            if (context.Type.Name == "Direction")
            {
                schema.Default = new OpenApiString(Convert.ToString(ExportFrom.MongoToElastic));
            }

        }
    }
}
