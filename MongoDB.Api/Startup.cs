using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.API.Extensions;
using MongoDB.API.Interface;
using MongoDB.API.Models;
using MongoDB.API.Services;

namespace MongoDB.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.Configure<BookstoreDatabaseSettings>(Configuration.GetSection(nameof(BookstoreDatabaseSettings)));
            //services.AddSingleton<IBookstoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);

            services.Configure<EmployeeDBSettings>(Configuration.GetSection(nameof(EmployeeDBSettings)));
            services.AddSingleton<IEmployeeDBSettings>(sp => sp.GetRequiredService<IOptions<EmployeeDBSettings>>().Value);
            services.AddSingleton<EmployeeService>();

            services.AddSwaggerConfiguration();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerConfig();
        }
    }
}
