using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Domain.Interfaces.Repositories
{
    public interface IAirportsRepository : IBaseRepository<AirportEntity>
    {
        Task<IEnumerable<AirportEntity>> SearchAirportsAsync(string searchTerm);
        Task SeedDataFromCsv(string csvFilePath);
    }
}
