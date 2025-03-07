using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Domain.Interfaces.Services
{
    public interface IAirlineSearchService
    {
        Task<IEnumerable<AirlineEntity>> SearchAirlinesAsync(string searchTerm, string? icao = null, string? type = null, string? country = null, string? name = null);
    }
}
