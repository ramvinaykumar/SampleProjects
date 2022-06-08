using Microsoft.EntityFrameworkCore;

namespace Application.Inventory.EFDBF.API.Models
{
    /// <summary>
    /// Tutorials Context Class 
    /// </summary>
    public partial class TutorialsContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TutorialsContext()
        {
        }

        /// <summary>
        /// Parameterized Constructor
        /// </summary>
        /// <param name="options"></param>
        public TutorialsContext(DbContextOptions<TutorialsContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Products
        /// </summary>
        public virtual DbSet<Products> Products { get; set; }

        /// <summary>
        /// User Info
        /// </summary>
        public virtual DbSet<UserInfo> UserInfo { get; set; }

        /// <summary>
        /// On Configuring
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder optionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        /// <summary>
        /// On Model Creating
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder modelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__B40CC6CD09783976");

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Color)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserInfo__1788CC4CA0F51561");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        /// <summary>
        /// On Model Creating Partial
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder modelBuilder</param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
