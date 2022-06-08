using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace MongoDB.API.Extensions
{
    /// <summary>
    /// Extension Class for Swagger implementation
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
                    Title = ".NET CORE WEB API With Mongo DB CRUD Operation",
                    Version = "V1",
                    Description = "API Application .NET CORE Architecture With Mongo DB CRUD Operation",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ram Vinay Kumar",
                        Email = "ramvinaykumar@hotmail.com",
                    }
                    //License =""
                });
                swagger.CustomSchemaIds(x => x.FullName);
                //swagger.OperationFilter<ProductDomainOperationFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);

                // To Enable authorization using Swagger (JWT)    
                //swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                //});

                //swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                }
                //            },
                //            new string[] {}

                //    }
                //});
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
                u.SwaggerEndpoint("/swagger/V1/swagger.json", "Application APIs");
                u.RoutePrefix = string.Empty;
                u.DefaultModelExpandDepth(0);
                u.DefaultModelsExpandDepth(-1);
            });
        }
    }
}
