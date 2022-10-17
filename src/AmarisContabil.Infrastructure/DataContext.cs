using AmarisContabil.Domain;
using Microsoft.EntityFrameworkCore;

namespace AmarisContabil.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Lancamento> Lancamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lancamento>()
                .Property(p => p.ValorBrl)
                .HasColumnType("decimal(18,4)");
        }
    }
}
