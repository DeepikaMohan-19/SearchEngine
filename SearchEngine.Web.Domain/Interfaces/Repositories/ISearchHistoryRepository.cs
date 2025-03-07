using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Domain.Interfaces.Repositories
{
    public interface ISearchHistoryRepository : IBaseRepository<SearchHistoryEntity>
    {
        Task<IEnumerable<SearchHistoryEntity>> GetByUser(string email);
    }
}
