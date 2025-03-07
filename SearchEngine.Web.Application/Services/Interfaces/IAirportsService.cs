using SearchEngine.Web.Application.DTOs;

namespace SearchEngine.Web.Application.Services.Interfaces
{
    public interface IAirportsService
    {
        Task<AirportDto?> GetAirportByIdAsync(Guid id);
        Task<IEnumerable<AirportDto>> GetAllAirportsAsync();
        Task<IEnumerable<AirportDto>> SearchAirportsAsync(string? email, string searchTerm, string? city = null, string? country = null, string? iata = null, string? icao = null, double? latitude = null, double? longitude = null, int? elevation = null, string? runwayLength = null, string? type = null, string? timezone = null, string? airportType = null, string? source = null, string? name = null);
        Task AddAirportAsync(AirportDto airport);
        Task UpdateAirportAsync(AirportDto airport);
        Task DeleteAirportAsync(Guid id);
    }
}
