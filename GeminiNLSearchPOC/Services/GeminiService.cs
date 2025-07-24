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
You are a SQL expert. Convert the following natural language query to a valid SQL query for a table named 'Documents' with these columns:
- Id (int)
- Title (string)
- EscrowOfficer (string)
- CreatedDate (datetime)
- FileName (string)

Return ONLY the SQL query with no explanations or formatting. Make sure it's valid SQLite syntax.

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