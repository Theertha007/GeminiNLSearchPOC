// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using GeminiNLSearchPOC.Models;

namespace GeminiNLSearchPOC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Document> Documents => Set<Document>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>().HasData(
                new Document { Id = 1, Title = "Property Deed", EscrowOfficer = "abc", CreatedDate = new DateTime(2025, 11, 5), FileName = "deed_001.pdf" },
                new Document { Id = 2, Title = "Mortgage Agreement", EscrowOfficer = "xyz", CreatedDate = DateTime.Now.AddDays(-15), FileName = "mortgage_002.pdf" },
                new Document { Id = 3, Title = "Title Insurance", EscrowOfficer = "abc", CreatedDate = DateTime.Now.AddDays(-5), FileName = "insurance_003.pdf" },
                new Document { Id = 4, Title = "Closing Statement", EscrowOfficer = "xyz", CreatedDate = DateTime.Now.AddDays(-45), FileName = "closing_004.pdf" }
            );
        }
    }
}