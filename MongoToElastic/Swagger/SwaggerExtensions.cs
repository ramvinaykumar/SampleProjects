using MongoToElastic.Filters;
using MongoToElastic.Models;

namespace MongoToElastic.Swagger
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = ApplicationConstants.AppName, Description = ApplicationConstants.ApiDescription });
                c.DocumentFilter<CustomSwaggerFilter>();
                c.SchemaFilter<SchemaFilter>();
            });
        }

        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(ApplicationConstants.SwaggerEndpoint, ApplicationConstants.AppName);
            });
        }
    }
}
