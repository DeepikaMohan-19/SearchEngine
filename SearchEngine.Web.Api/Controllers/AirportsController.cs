using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchEngine.Web.Application.DTOs;
using SearchEngine.Web.Application.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace SearchEngine.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController(IAirportsService airportsService) : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "UserOrAdmin")]
        [ProducesResponseType(typeof(IEnumerable<AirportDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AirportDto>>> Search(
    [FromQuery][SwaggerParameter("The search term.")] string searchTerm,
    [FromQuery][SwaggerParameter("The city to filter by.")] string? city = null,
    [FromQuery][SwaggerParameter("The country to filter by.")] string? country = null,
    [FromQuery][SwaggerParameter("The IATA code to filter by.")] string? iata = null,
    [FromQuery][SwaggerParameter("The ICAO code to filter by.")] string? icao = null,
    [FromQuery][SwaggerParameter("The latitude to filter by.")] double? latitude = null,
    [FromQuery][SwaggerParameter("The longitude to filter by.")] double? longitude = null,
    [FromQuery][SwaggerParameter("The elevation to filter by.")] int? elevation = null,
    [FromQuery][SwaggerParameter("The runway length to filter by.")] string? runwayLength = null,
    [FromQuery][SwaggerParameter("The type to filter by.")] string? type = null,
    [FromQuery][SwaggerParameter("The timezone to filter by.")] string? timezone = null,
    [FromQuery][SwaggerParameter("The airport type to filter by.")] string? airportType = null,
    [FromQuery][SwaggerParameter("The source to filter by.")] string? source = null,
    [FromQuery][SwaggerParameter("The name to filter by.")] string? name = null)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var searchHistory = await airportsService.SearchAirportsAsync(userEmail,
                searchTerm, city, country, iata, icao, latitude, longitude, elevation,
                runwayLength, type, timezone, airportType, source, name);

            return Ok(searchHistory);
        }
    }
}
