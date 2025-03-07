using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Domain.Interfaces.Repositories
{
    public interface IAirlinesRepository : IBaseRepository<AirlineEntity>
    {
        Task<IEnumerable<AirlineEntity>> SearchAirlinesAsync(string searchTerm);
        Task SeedDataFromCsv(string csvFilePath);
    }
}
