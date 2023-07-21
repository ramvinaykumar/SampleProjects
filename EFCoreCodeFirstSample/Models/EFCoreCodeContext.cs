using EFCoreCodeFirstSample.Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirstSample.Models
{
    public class EFCoreCodeContext : DbContext
    {
        public EFCoreCodeContext()
        {
        }
        public EFCoreCodeContext(DbContextOptions<EFCoreCodeContext> options)
            : base(options)
        {
        }

        public DbSet<MemberContactDetailsEntity> MemberContactDetails { get; set; }

        public DbSet<EmployeeEntity> Employees { get; set; }
    }
}
