using EsAppTest.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EsAppTest.Infrastructure.Data
{
    public sealed class EsAppTestDbContext : DbContext
    {
        public EsAppTestDbContext(DbContextOptions<EsAppTestDbContext> options) : base(options) { }

        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(p =>
            {
                p.ToTable("Payments");
                p.HasKey(x => x.PaymentId);

                p.Property(x => x.ServiceProvider).HasMaxLength(200).IsRequired();
                p.Property(x => x.Currency).HasMaxLength(3).IsRequired();
                p.Property(x => x.Amount).HasColumnType("decimal(18,2)").IsRequired();
                p.Property(x => x.Status).IsRequired();

                p.HasIndex(x => x.CustomerId);
                p.HasIndex(x => x.CreatedAt);
            });
        }
    }
}
