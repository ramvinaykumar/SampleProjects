using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebAPI.JWT.Authentication
{
    /// <summary>
    /// Main class for Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method of the class
        /// </summary>
        /// <param name="args">string[] args</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create Host Builder
        /// </summary>
        /// <param name="args">string[] args</param>
        /// <returns>Returns IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
