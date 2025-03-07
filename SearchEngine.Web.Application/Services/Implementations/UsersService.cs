using AutoMapper;
using SearchEngine.Web.Application.DTOs.Authentication;
using SearchEngine.Web.Application.Services.Interfaces;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Repositories;

namespace SearchEngine.Web.Application.Services.Implementations
{
    public class UsersService(IUsersRepository usersRepository, IMapper mapper) : IUsersService
    {
        public async Task Add(UserDto user) => await usersRepository.AddAsync(mapper.Map<UserEntity>(user));

        public async Task Delete(Guid id) => await usersRepository.DeleteAsync(id);

        public async Task<IEnumerable<UserDto>> GetAll() =>
            mapper.Map<IEnumerable<UserDto>>(await usersRepository.GetAllAsync());

        public async Task<UserDto?> GetByEmail(string email) =>
            mapper.Map<UserDto?>(await usersRepository.GetByEmail(email));

        public async Task<UserDto?> GetByEmailAndPassword(string email, string password) =>
            mapper.Map<UserDto?>(await usersRepository.GetByEmailAndPassword(email, password));

        public async Task<UserDto?> GetById(Guid id) =>
            mapper.Map<UserDto?>(await usersRepository.GetByIdAsync(id));

        public async Task Update(UserDto user) => await usersRepository.UpdateAsync(mapper.Map<UserEntity>(user));
    }
}
