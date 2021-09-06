using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RandomQuotes.Data;
using RandomQuotes.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RandomQuotes.Data
{
    public class EntitiesContext : DbContext
    {
        public EntitiesContext()
        {

        }
        public EntitiesContext(DbContextOptions<EntitiesContext>options) :
            base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("QuoteConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        // Adding initial data to the database durimg Migration and first update
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }

        // Makiing CreatedAt and UpdatedAt to be Automatically into thr database
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || 
                            e.State == EntityState.Modified));
            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }

        public DbSet<QuotesViewModel> Quotes { get; set; }
        
    }
}
