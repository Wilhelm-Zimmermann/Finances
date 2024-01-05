using FinanceController.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceController.Domain.Infra.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<BillType?> BillTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }

        protected DatabaseContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillType>(entity =>
            {
                entity.ToTable("bill_types");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Type).IsRequired();
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bills");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired();
                entity.Property(a => a.Price).IsRequired();
                entity.Property(a => a.Description);
                entity.Property(a => a.PaidDate);
                entity.HasOne(a => a.BillType).WithMany(x => x.Bills).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(a => a.User).WithMany(x => x.Bills).OnDelete(DeleteBehavior.Cascade);                
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired();
                entity.Property(a => a.Email).IsRequired();
                entity.Property(a => a.Password).IsRequired();
                entity.Property(a => a.Role).IsRequired();
            });
        }
    }
}
