using Microsoft.EntityFrameworkCore;
using SistemaLocacao.Models;
using SistemaLocacao.Models.Reports;

namespace SistemaLocacao.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<ReportLocation> ReportsLocation { get; set; }
        public DbSet<ReportMovie> ReportsMovie { get; set; }
        public DbSet<ReportClient> ReportsClient { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasIndex(c => new { c.CPF, c.Name })
                .IsUnique();

            modelBuilder.Entity<Movie>()
                .HasIndex(m => new { m.Lounch, m.Title })
                .IsUnique();

            modelBuilder.Entity<Location>()
                .HasOne<Movie>()
                .WithMany()
                .HasForeignKey(l => l.MovieId);

            modelBuilder.Entity<Location>()
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(l => l.ClientId);

            modelBuilder.Entity<ReportLocation>()
                .HasNoKey();
        }
    }
}
