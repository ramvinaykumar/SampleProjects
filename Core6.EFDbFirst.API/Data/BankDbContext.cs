using Core6.EFDbFirst.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core6.EFDbFirst.API.Data
{
    public class BankDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public BankDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<BankAccount> BankAccount { get; set; }

        public DbSet<BankAccountDto> BankAccounts { get; set; }
    }
}
