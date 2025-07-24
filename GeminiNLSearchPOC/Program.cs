// Program.cs
using Microsoft.EntityFrameworkCore;
using GeminiNLSearchPOC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddScoped<IGeminiService, GeminiService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=documents.db"));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

app.Run();

// Models and DbContext in same file for simplicity
public class Document
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string EscrowOfficer { get; set; } = "";
    public DateTime CreatedDate { get; set; }
    public string FileName { get; set; } = "";
}

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