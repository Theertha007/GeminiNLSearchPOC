// Program.cs
using Microsoft.EntityFrameworkCore;
using GeminiNLSearchPOC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
//builder.Services.AddScoped<IGeminiService, GeminiService>();
var provider = builder.Configuration["AI:Provider"]; // "Gemini" | "AzureOpenAI"
builder.Services.AddScoped<INaturalLanguageQueryBuilder>(
    provider switch
    {
        "AzureOpenAI" => sp => sp.GetRequiredService<AzureOpenAIQueryBuilder>(),
        _             => sp => sp.GetRequiredService<GeminiQueryBuilder>()
    });

builder.Services.AddScoped<AzureOpenAIQueryBuilder>();
builder.Services.AddScoped<GeminiQueryBuilder>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=documents.db"));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

// Initialize database
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    context.Database.EnsureCreated();
//}

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
            // Original Set
            new Document { Id = 1, Title = "Property Deed", EscrowOfficer = "abc", CreatedDate = new DateTime(2025, 11, 5), FileName = "deed_001.pdf" },
            new Document { Id = 2, Title = "Mortgage Agreement", EscrowOfficer = "xyz", CreatedDate = new DateTime(2025, 7, 10), FileName = "mortgage_002.pdf" },
            new Document { Id = 3, Title = "Title Insurance", EscrowOfficer = "abc", CreatedDate = new DateTime(2025, 7, 20), FileName = "insurance_003.pdf" },
            new Document { Id = 4, Title = "Closing Statement", EscrowOfficer = "xyz", CreatedDate = new DateTime(2025, 6, 10), FileName = "closing_004.pdf" },
            new Document { Id = 5, Title = "Loan Estimate", EscrowOfficer = "pqr", CreatedDate = new DateTime(2025, 6, 25), FileName = "loan_est_005.pdf" },
            new Document { Id = 6, Title = "Promissory Note", EscrowOfficer = "abc", CreatedDate = new DateTime(2024, 7, 25), FileName = "promo_note_006.pdf" },
            new Document { Id = 7, Title = "Inspection Report", EscrowOfficer = "xyz", CreatedDate = new DateTime(2025, 4, 26), FileName = "inspection_007.pdf" },
            new Document { Id = 8, Title = "Appraisal Report", EscrowOfficer = "pqr", CreatedDate = new DateTime(2024, 1, 20), FileName = "appraisal_008.pdf" },
            new Document { Id = 9, Title = "HUD-1 Settlement", EscrowOfficer = "abc", CreatedDate = new DateTime(2025, 12, 10), FileName = "hud_settlement_009.pdf" },
            new Document { Id = 10, Title = "Earnest Money Receipt", EscrowOfficer = "xyz", CreatedDate = new DateTime(2025, 1, 26), FileName = "receipt_010.pdf" },
            new Document { Id = 11, Title = "Addendum A", EscrowOfficer = "abc", CreatedDate = new DateTime(2025, 11, 21), FileName = "addendum_A_011.pdf" },
            new Document { Id = 12, Title = "Final Disclosures", EscrowOfficer = "pqr", CreatedDate = new DateTime(2025, 7, 23), FileName = "disclosures_012.pdf" },

            // Expanded Set
            new Document { Id = 13, Title = "Purchase Agreement", EscrowOfficer = "lmn", CreatedDate = new DateTime(2025, 7, 25), FileName = "purchase_agree_013.pdf" },
            new Document { Id = 14, Title = "Revised Loan Statement", EscrowOfficer = "abc", CreatedDate = new DateTime(2025, 7, 18), FileName = "revised_loan_014.pdf" },
            new Document { Id = 15, Title = "Homeowner's Insurance Policy", EscrowOfficer = "xyz", CreatedDate = new DateTime(2025, 6, 10), FileName = "ho_policy_015.pdf" },
            new Document { Id = 16, Title = "Tax Statement 2024", EscrowOfficer = "pqr", CreatedDate = new DateTime(2023, 7, 25), FileName = "tax_2024_016.pdf" },
            new Document { Id = 17, Title = "Water Rights Agreement", EscrowOfficer = "lmn", CreatedDate = new DateTime(2024, 9, 28), FileName = "water_rights_017.pdf" },
            new Document { Id = 18, Title = "HOA Bylaws", EscrowOfficer = "abc", CreatedDate = new DateTime(2025, 11, 15), FileName = "hoa_bylaws_018.pdf" },
            new Document { Id = 19, Title = "Survey Report", EscrowOfficer = "pqr", CreatedDate = new DateTime(2025, 7, 5), FileName = "survey_report_019.pdf" },
            new Document { Id = 20, Title = "Radon Inspection Report", EscrowOfficer = "xyz", CreatedDate = new DateTime(2025, 6, 20), FileName = "radon_report_020.pdf" },
            new Document { Id = 21, Title = "Lease Agreement", EscrowOfficer = "lmn", CreatedDate = new DateTime(2024, 3, 14), FileName = "lease_agree_021.pdf" },
            new Document { Id = 22, Title = "Final Walkthrough Statement", EscrowOfficer = "abc", CreatedDate = new DateTime(2025, 7, 22), FileName = "walkthrough_022.pdf" },
            new Document { Id = 23, Title = "Repair Agreement", EscrowOfficer = "pqr", CreatedDate = new DateTime(2025, 11, 28), FileName = "repair_agree_023.pdf" },
            new Document { Id = 24, Title = "Contingency Removal Form", EscrowOfficer = "xyz", CreatedDate = new DateTime(2025, 7, 15), FileName = "contingency_024.pdf" },
            new Document { Id = 25, Title = "Power of Attorney Document", EscrowOfficer = "lmn", CreatedDate = new DateTime(2022, 7, 25), FileName = "poa_doc_025.pdf" }
        );
    }
}