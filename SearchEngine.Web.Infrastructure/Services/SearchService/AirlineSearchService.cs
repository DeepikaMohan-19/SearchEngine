using Azure.Search.Documents;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Services;

namespace SearchEngine.Web.Infrastructure.Services.SearchService
{
    public class AirlineSearchService(string indexName, string endpoint, string adminKey) : BaseSearchService(indexName, endpoint, adminKey), IAirlineSearchService
    {
        public async Task<IEnumerable<AirlineEntity>> SearchAirlinesAsync(string searchTerm, string? icao = null, string? type = null, string? country = null, string? name = null)
        {
            var searchOptions = new SearchOptions
            {
                IncludeTotalCount = true,
                Filter = this.BuildFilterExpression(icao, type, country, name)
            };

            var searchResults = await SearchClient.SearchAsync<AirlineEntity>(searchTerm, searchOptions);
            return searchResults.Value.GetResults().OrderByDescending(x => x.Score).Select(r => r.Document);
        }

        public string BuildFilterExpression(string? icao, string? type, string? country, string? name)
        {
            var filterExpressions = new List<string>();

            if (!string.IsNullOrWhiteSpace(icao))
                filterExpressions.Add($"Icao eq '{icao}'");

            if (!string.IsNullOrWhiteSpace(type))
                filterExpressions.Add($"Type eq '{type}'");

            if (!string.IsNullOrWhiteSpace(country))
                filterExpressions.Add($"Country eq '{country}'");

            if (!string.IsNullOrWhiteSpace(name))
                filterExpressions.Add($"Name eq '{name}'");

            return string.Join(" and ", filterExpressions);
        }
    }
}
