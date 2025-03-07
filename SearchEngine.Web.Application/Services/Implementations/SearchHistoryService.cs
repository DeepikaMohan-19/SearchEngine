using AutoMapper;
using SearchEngine.Web.Application.DTOs;
using SearchEngine.Web.Application.Services.Interfaces;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Repositories;

namespace SearchEngine.Web.Application.Services.Implementations
{
    public class SearchHistoryService(ISearchHistoryRepository searchHistoryRepository, IMapper mapper) : ISearchHistoryService
    {
        public async Task<IEnumerable<SearchHistoryDto>> GetAllSearchHistoriesAsync()
        {
            var searchHistoryEntities = await searchHistoryRepository.GetAllAsync();
            return mapper.Map<IEnumerable<SearchHistoryDto>>(searchHistoryEntities);
        }

        public async Task<IEnumerable<SearchHistoryDto>> GetSearchHistoriesByUserAsync(string email)
        {
            var searchHistoryEntities = await searchHistoryRepository.GetByUser(email);
            return mapper.Map<IEnumerable<SearchHistoryDto>>(searchHistoryEntities);
        }

        public async Task AddSearchHistoryAsync(SearchHistoryDto searchHistoryDto)
        {
            var searchHistoryEntity = mapper.Map<SearchHistoryEntity>(searchHistoryDto);
            await searchHistoryRepository.AddAsync(searchHistoryEntity);
        }
        public async Task UpdateSearchHistoryAsync(SearchHistoryDto searchHistoryDto)
        {
            var searchHistoryEntity = mapper.Map<SearchHistoryEntity>(searchHistoryDto);
            await searchHistoryRepository.UpdateAsync(searchHistoryEntity);
        }
        public async Task DeleteSearchHistoryAsync(Guid id)
        {
            await searchHistoryRepository.DeleteAsync(id);
        }
    }
}
