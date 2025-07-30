// Services/GeminiService.cs
using System.Text;
using System.Text.Json;

namespace GeminiNLSearchPOC.Services
{
    public interface IGeminiService
    {
        Task<string> GenerateSqlQueryAsync(string naturalLanguageQuery);
    }

    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["Gemini:ApiKey"] ?? throw new ArgumentNullException("API Key not found");
        }

        public async Task<string> GenerateSqlQueryAsync(string naturalLanguageQuery)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash-lite:generateContent?key={_apiKey}";

            var prompt = $@"
You are an expert SQL developer working with a SQLite database. Convert the following natural language query to a valid SQLite query.

DATABASE SCHEMA:
Table: Documents
Columns:
- Id (INTEGER, primary key)
- Title (TEXT)
- EscrowOfficer (TEXT)
- CreatedDate (DATETIME)
- FileName (TEXT)

IMPORTANT RULES:
1. Return ONLY the SQL query, no explanations
2. Use SQLite syntax (e.g., date functions like strftime)
3. Always use SELECT statements
4. Never include DELETE, UPDATE, INSERT, DROP, or ALTER
5. Make sure the query is safe and valid
6. Use proper column names as shown above
7. For date comparisons, use strftime function
8. Return all columns in the SELECT statement

EXAMPLES:
User: Show me all records from November 2025
SQL: SELECT * FROM Documents WHERE strftime('%Y-%m', CreatedDate) = '2025-11';

User: Find records by officer abc
SQL: SELECT * FROM Documents WHERE EscrowOfficer = 'abc';

User: Show files from last 30 days
SQL: SELECT * FROM Documents WHERE CreatedDate >= date('now', '-30 days');

User: Show records by xyz last month
SQL: SELECT * FROM Documents WHERE EscrowOfficer = 'xyz' AND strftime('%Y-%m', CreatedDate) = strftime('%Y-%m', 'now', '-1 month');

Now convert this query:
Natural language query: {naturalLanguageQuery}";

            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        parts = new[] {
                            new { text = prompt }
                        }
                    }
                },
                generationConfig = new {
                    temperature = 0.2,
                    maxOutputTokens = 256
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API error: {responseContent}");
            }

            using var doc = JsonDocument.Parse(responseContent);
            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text?.Trim() ?? "";
        }
    }
}