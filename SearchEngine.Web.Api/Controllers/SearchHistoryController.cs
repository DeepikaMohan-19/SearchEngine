using Microsoft.AspNetCore.Mvc;
using SearchEngine.Web.Application.Services.Interfaces;
using System.Security.Claims;

namespace SearchEngine.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHistoryController(ISearchHistoryService searchHistoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string[]>> GetSearchHistoryByUser()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var searchHistories = await searchHistoryService.GetSearchHistoriesByUserAsync(userEmail ?? string.Empty);
            return Ok(searchHistories.Select(x => x.SearchTerm));
        }
    }
}
