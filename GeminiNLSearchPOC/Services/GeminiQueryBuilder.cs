using System.Text;
using System.Text.Json;

namespace GeminiNLSearchPOC.Services;

public sealed class GeminiQueryBuilder : INaturalLanguageQueryBuilder
{
    private readonly HttpClient _httpClient = new();
    private readonly string _apiKey;

    public GeminiQueryBuilder(IConfiguration cfg)
        => _apiKey = cfg["Gemini:ApiKey"]
                     ?? throw new ArgumentNullException("Gemini:ApiKey");

    public async Task<string> GenerateSqlQueryAsync(string nl)
    {
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-lite:generateContent?key={_apiKey}";

        var prompt = $"""
            You are an expert SQL developer working with a SQLite database.
            Return ONLY the raw SQL query, no markdown fences, no explanations.
            Table: Documents  (columns: Id, Title, EscrowOfficer, CreatedDate, FileName)

            Natural language request: {nl}
            """;

        var payload = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = prompt } } }
            },
            generationConfig = new { temperature = 0.2, maxOutputTokens = 256 }
        };

        var json = JsonSerializer.Serialize(payload);
        var res  = await _httpClient.PostAsync(url,
                      new StringContent(json, Encoding.UTF8, "application/json"));
        res.EnsureSuccessStatusCode();

        var body = await res.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(body);
        return doc.RootElement
                  .GetProperty("candidates")[0]
                  .GetProperty("content")
                  .GetProperty("parts")[0]
                  .GetProperty("text")
                  .GetString() ?? "";
    }
}