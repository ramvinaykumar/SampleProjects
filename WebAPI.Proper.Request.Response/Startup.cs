using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using WebAPI.Proper.Request.Response.Common.AppDbContext;
using WebAPI.Proper.Request.Response.Common.Constants;
using WebAPI.Proper.Request.Response.Repository.Interface;
using WebAPI.Proper.Request.Response.Repository.Services;

namespace WebAPI.Proper.Request.Response
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApplicationDbContext(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = ApplicationConstants.AppName, Description = ApplicationConstants.ApiDescription, Version = ApplicationConstants.ApiVersion });
                c.ResolveConflictingActions(apiDescription => apiDescription.First());
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
            services.AddTransient<IStudentServices, StudentServices>();
            services.AddTransient<ICampaignService, CampaignService>();
            services.AddTransient<ICountryDataService, CountryDataService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(ApplicationConstants.SwaggerEndpoint, ApplicationConstants.AppName + " " + ApplicationConstants.ApiVersion));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowAnyOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
