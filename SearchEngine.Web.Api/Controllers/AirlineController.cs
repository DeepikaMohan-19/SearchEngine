using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchEngine.Web.Application.Services.Interfaces;
using SearchEngine.Web.Domain.Entites;
using Swashbuckle.AspNetCore.Annotations;

namespace SearchEngine.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirlineController(IAirlinesService airlineService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "UserOrAdmin")]
        [ProducesResponseType(typeof(IEnumerable<AirlineEntity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search(
     [FromQuery][SwaggerParameter("The search term.")] string searchTerm,
     [FromQuery][SwaggerParameter("The ICAO code to filter by.")] string? icao = null,
     [FromQuery][SwaggerParameter("The type to filter by.")] string? type = null,
     [FromQuery][SwaggerParameter("The country to filter by.")] string? country = null,
     [FromQuery][SwaggerParameter("The name to filter by.")] string? name = null)
        {
            var searchHistory = await airlineService.SearchAirlinesAsync(
                searchTerm, icao, type, country, name);

            return Ok(searchHistory);
        }
    }
}
