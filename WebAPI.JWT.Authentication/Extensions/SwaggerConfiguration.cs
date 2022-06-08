using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace WebAPI.JWT.Authentication.Extensions
{
    /// <summary>
    /// Class for swagger configuration
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Add Swagger Configuration Using Method Extension
        /// </summary>
        /// <param name="services">IServiceCollection services</param>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "ASP.NET Core JWT Authentication API",
                    Version = "V1",
                    Description = "ASP.NET Core JWT Authentication API",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ram Vinay Kumar",
                        Email = "ramvinaykumar@hotmail.com",
                    }
                    //License =""
                });
                swagger.CustomSchemaIds(x => x.FullName);
                // swagger.OperationFilter<ProductDomainOperationFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Use Swagger Config Using Method Extension
        /// </summary>
        /// <param name="app">IApplicationBuilder app</param>
        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(u =>
            {
                u.SwaggerEndpoint("/swagger/V1/swagger.json", "JWT Authentication API");
                u.RoutePrefix = string.Empty;
                u.DefaultModelExpandDepth(0);
                u.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
