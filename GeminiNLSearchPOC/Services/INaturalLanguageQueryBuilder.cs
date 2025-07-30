namespace GeminiNLSearchPOC.Services;

public interface INaturalLanguageQueryBuilder
{
    Task<string> GenerateSqlQueryAsync(string naturalLanguageQuery);
}