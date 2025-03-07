using SearchEngine.Web.Application.DTOs;

namespace SearchEngine.Web.Application.Services.Interfaces
{
    public interface ISearchHistoryService
    {
        Task<IEnumerable<SearchHistoryDto>> GetAllSearchHistoriesAsync();
        Task AddSearchHistoryAsync(SearchHistoryDto searchHistoryDto);
        Task UpdateSearchHistoryAsync(SearchHistoryDto searchHistoryDto);
        Task DeleteSearchHistoryAsync(Guid id);
        Task<IEnumerable<SearchHistoryDto>> GetSearchHistoriesByUserAsync(string email);
    }
}
