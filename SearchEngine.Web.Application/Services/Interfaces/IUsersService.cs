using SearchEngine.Web.Application.DTOs.Authentication;

namespace SearchEngine.Web.Application.Services.Interfaces
{
    public interface IUsersService
    {
        Task<UserDto?> GetById(Guid id);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto?> GetByEmail(string email);
        Task Add(UserDto user);
        Task Update(UserDto user);
        Task Delete(Guid id);
        Task<UserDto?> GetByEmailAndPassword(string email, string password);
    }
}
