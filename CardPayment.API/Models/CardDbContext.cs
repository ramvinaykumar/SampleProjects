using Microsoft.EntityFrameworkCore;

namespace CardPayment.API.Models
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(DbContextOptions<CardDbContext> options) : base(options)
        {

        }

        public DbSet<CardDetail> CardDetails { get; set; }
    }
}
