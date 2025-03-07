using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Domain.Interfaces.Services
{
    public interface IAirportSearchService
    {
        Task<IEnumerable<AirportEntity>> SearchAirportsAsync(string searchTerm, string? city = null, string? country = null, string? iata = null, string? icao = null, double? latitude = null, double? longitude = null, int? elevation = null, string? runwayLength = null, string? type = null, string? timezone = null, string? airportType = null, string? source = null, string? name = null);
    }
}
