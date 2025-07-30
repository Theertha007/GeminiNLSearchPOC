using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
//using Azure.Identity;            // only if you switch to Entra ID later
using Azure;
using Azure.AI.OpenAI;

namespace GeminiNLSearchPOC.Services;

public sealed class AzureOpenAIQueryBuilder : INaturalLanguageQueryBuilder
{
    private readonly ChatClient _chatClient;

    public AzureOpenAIQueryBuilder(IConfiguration cfg)
    {
        var endpoint   = cfg["AzureOpenAI:Endpoint"];
        var deployment = cfg["AzureOpenAI:Deployment"];
        var key        = cfg["AzureOpenAI:Key"];

        // For key-based auth (AI Foundry)
        var credential = new AzureKeyCredential(key!);
        _chatClient = new AzureOpenAIClient(new Uri(endpoint!), credential)
                      .GetChatClient(deployment!);
    }

    public async Task<string> GenerateSqlQueryAsync(string nl)
    {
        var prompt = $"""
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
                Natural language query: {nl}
            """;

        var messages = new ChatMessage[]
        {
            new SystemChatMessage("You are a SQL expert."),
            new UserChatMessage(prompt)
        };

        var completion = await _chatClient.CompleteChatAsync(messages);
        return completion.Value.Content[0].Text
                          .Replace("```sql", "")
                          .Replace("```", "")
                          .Trim();
    }
}