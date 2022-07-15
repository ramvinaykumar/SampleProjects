using Microsoft.EntityFrameworkCore;
using WebAPI.Proper.Request.Response.Models.Campaign;
using WebAPI.Proper.Request.Response.Models.Students;

namespace WebAPI.Proper.Request.Response.Common.AppDbContext
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Students> Students { get; set; }

        public DbSet<Campaigns> Campaign { get; set; }
    }
}
