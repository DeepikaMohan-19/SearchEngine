using SearchEngine.Web.Application.DTOs;

namespace SearchEngine.Web.Application.Services.Interfaces
{
    public interface IAirlinesService
    {
        Task<AirlineDto?> GetAirlineByIdAsync(Guid id);
        Task<IEnumerable<AirlineDto>> GetAllAirlinesAsync();
        Task<IEnumerable<AirlineDto>> SearchAirlinesAsync(string searchTerm, string? icao = null, string? type = null, string? country = null, string? name = null);
        Task AddAirlineAsync(AirlineDto airline);
        Task UpdateAirlineAsync(AirlineDto airline);
        Task DeleteAirlineAsync(Guid id);
    }
}
