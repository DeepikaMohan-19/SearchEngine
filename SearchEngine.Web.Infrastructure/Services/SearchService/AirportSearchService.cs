using Azure.Search.Documents;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Services;

namespace SearchEngine.Web.Infrastructure.Services.SearchService
{
    public class AirportSearchService(string indexName, string endpoint, string adminKey) : BaseSearchService(indexName, endpoint, adminKey), IAirportSearchService
    {
        public async Task<IEnumerable<AirportEntity>> SearchAirportsAsync(string searchTerm, string? city = null, string? country = null, string? iata = null, string? icao = null, double? latitude = null, double? longitude = null, int? elevation = null, string? runwayLength = null, string? type = null, string? timezone = null, string? airportType = null, string? source = null, string? name = null)
        {
            var searchOptions = new SearchOptions
            {
                IncludeTotalCount = true,
                Size = 10,
                Filter = BuildFilterExpression(city, country, iata, icao, latitude, longitude, elevation, runwayLength, type, timezone, airportType, source, name),
            };

            var searchResults = await SearchClient.SearchAsync<AirportEntity>(searchTerm, searchOptions);
            return searchResults.Value.GetResults().OrderByDescending(x => x.Score).Select(r => r.Document);
        }

        private string BuildFilterExpression(string? city, string? country, string? iata, string? icao, double? latitude, double? longitude, int? elevation, string? runwayLength, string? type, string? timezone, string? airportType, string? source, string? name)
        {
            var filterExpressions = new List<string>();

            if (!string.IsNullOrWhiteSpace(city))
                filterExpressions.Add($"City eq '{city}'");

            if (!string.IsNullOrWhiteSpace(country))
                filterExpressions.Add($"Country eq '{country}'");

            if (!string.IsNullOrWhiteSpace(iata))
                filterExpressions.Add($"Iata eq '{iata}'");

            if (!string.IsNullOrWhiteSpace(icao))
                filterExpressions.Add($"Icao eq '{icao}'");

            if (latitude != null)
                filterExpressions.Add($"Latitude eq {latitude}");

            if (longitude != null)
                filterExpressions.Add($"Longitude eq {longitude}");

            if (elevation != null)
                filterExpressions.Add($"Elevation eq {elevation}");

            if (!string.IsNullOrWhiteSpace(runwayLength))
                filterExpressions.Add($"RunwayLength eq '{runwayLength}'");

            if (!string.IsNullOrWhiteSpace(type))
                filterExpressions.Add($"Type eq '{type}'");

            if (!string.IsNullOrWhiteSpace(timezone))
                filterExpressions.Add($"Timezone eq '{timezone}'");

            if (!string.IsNullOrWhiteSpace(airportType))
                filterExpressions.Add($"AirportType eq '{airportType}'");

            if (!string.IsNullOrWhiteSpace(source))
                filterExpressions.Add($"Source eq '{source}'");

            if (!string.IsNullOrWhiteSpace(name))
                filterExpressions.Add($"Name eq '{name}'");

            return string.Join(" and ", filterExpressions);
        }
    }
}
