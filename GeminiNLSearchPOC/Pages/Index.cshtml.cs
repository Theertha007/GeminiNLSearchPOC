// Pages/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GeminiNLSearchPOC.Services;
using System.Text.RegularExpressions;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IGeminiService _geminiService;

    public IndexModel(AppDbContext context, IGeminiService geminiService)
    {
        _context = context;
        _geminiService = geminiService;
    }

    public IList<Document> Documents { get; set; } = new List<Document>();
    public string Message { get; set; } = "";
    public string SqlQuery { get; set; } = "";
    public string Query { get; set; } = "";

    public async Task OnGetAsync()
    {
        Documents = await _context.Documents.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync(string query)
    {
        Query = query ?? "";

        if (string.IsNullOrWhiteSpace(query))
        {
            Documents = await _context.Documents.ToListAsync();
            return Page();
        }

        try
        {
            // Get SQL from Gemini AI
            SqlQuery = await _geminiService.GenerateSqlQueryAsync(query);
            
            // Clean the SQL query (remove markdown formatting)
            var cleanedSql = CleanSqlQuery(SqlQuery);
            Message = $"Processed query: '{query}'";

            // Execute the cleaned SQL query
            Documents = await _context.Documents.FromSqlRaw(cleanedSql).ToListAsync();
        }
        catch (Exception ex)
        {
            Message = $"Error: {ex.Message}";
            Documents = await _context.Documents.ToListAsync();
        }

        return Page();
    }

    private string CleanSqlQuery(string sql)
    {
        // Remove markdown code block formatting
        sql = Regex.Replace(sql, @"^```[a-zA-Z]*", "", RegexOptions.Multiline);
        sql = Regex.Replace(sql, @"```$", "", RegexOptions.Multiline);
        
        // Trim whitespace
        sql = sql.Trim();
        
        // Basic validation - ensure it's a SELECT statement
        if (!sql.ToUpper().StartsWith("SELECT"))
        {
            throw new InvalidOperationException("Invalid SQL query generated");
        }
        
        return sql;
    }
}