using Microsoft.EntityFrameworkCore;
using Cards.Client.Models;

namespace Cards.Service.Context
{
    public class CardContext : DbContext
    {
        public virtual DbSet<Card> Cards { get; set; }
		public virtual DbSet<ImageUri> ImageUris { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Card>()
				.Property(a => a.Id).IsConcurrencyToken();

			modelBuilder.Entity<ImageUri>()
				.Property(a => a.Id).IsConcurrencyToken();

			modelBuilder.Entity<ImageUri>()
				.HasKey(a => new { a.Id });
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = ConfigurationManager.Configuration.GetConnectionString("Hiroku");
            optionsBuilder.EnableSensitiveDataLogging();
            var connectionString = "User ID=jqoleiwwdbvcnf;Password=247091fbf2579044d7f48585703d54e5ea941e7b2600cdee0967d3cfa51c4fbd;Host=ec2-54-83-203-198.compute-1.amazonaws.com;Port=5432;Database=d8651d20v8iq3h;Pooling=true;Use SSL Stream=True;SSL Mode=Require;TrustServerCertificate=True;";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}