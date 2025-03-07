using Microsoft.EntityFrameworkCore;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Repositories;
using SearchEngine.Web.Infrastructure.Persistance;

namespace SearchEngine.Web.Infrastructure.Repositories
{
    public class SearchHistoryRepository(IDbContextFactory<FlightsDBContext> dbContextFactory) : BaseRepository<SearchHistoryEntity>(dbContextFactory), ISearchHistoryRepository
    {
        public async Task<IEnumerable<SearchHistoryEntity>> GetByUser(string email)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.UserSearchHistory.Where(x => !string.IsNullOrWhiteSpace(x.CreatedByEmail) && x.CreatedByEmail.Contains(email)).ToListAsync();
        }
    }
}
