using Microsoft.EntityFrameworkCore;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Repositories;
using SearchEngine.Web.Infrastructure.Persistance;

namespace SearchEngine.Web.Infrastructure.Repositories
{
    public class UsersRepository(IDbContextFactory<FlightsDBContext> dbContextFactory) : BaseRepository<UserEntity>(dbContextFactory), IUsersRepository
    {
        public async Task<UserEntity?> GetByEmail(string email)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserEntity?> GetByEmailAndPassword(string email, string password)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
