using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Cards.Models;
using QueueHandler;
using System.Net.Http;

namespace Cards
{
    public class CardContext : DbContext
    {
        public virtual DbSet<Card> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.Configuration.GetConnectionString("Scryfall");
            var _httpRequest = new HttpRequestMessage();
            //HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new System.Uri(new System.Uri(connectionString + "c3dab325-8f4f-4288-9f3f-960e52b4335b").ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasKey(a => new { a.Id });
        }
    }
}