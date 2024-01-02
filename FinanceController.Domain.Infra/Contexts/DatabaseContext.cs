using FinanceController.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceController.Domain.Infra.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<BillType?> BillTypes { get; set; }

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
        }
    }
}
