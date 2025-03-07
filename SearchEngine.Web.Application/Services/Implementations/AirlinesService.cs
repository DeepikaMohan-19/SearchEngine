using AutoMapper;
using SearchEngine.Web.Application.DTOs;
using SearchEngine.Web.Application.Services.Interfaces;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Repositories;
using SearchEngine.Web.Domain.Interfaces.Services;

namespace SearchEngine.Web.Application.Services.Implementations
{
    public class AirlinesService(IAirlinesRepository airlineRepository, IAirlineSearchService airlineSearchService, IMapper mapper) : IAirlinesService
    {
        public async Task<AirlineDto?> GetAirlineByIdAsync(Guid id) =>
            mapper.Map<AirlineDto>(await airlineRepository.GetByIdAsync(id));

        public async Task<IEnumerable<AirlineDto>> GetAllAirlinesAsync() =>
            mapper.Map<IEnumerable<AirlineDto>>(await airlineRepository.GetAllAsync());

        public async Task<IEnumerable<AirlineDto>> SearchAirlinesAsync(string searchTerm, string? icao = null, string? type = null, string? country = null, string? name = null) =>
            mapper.Map<IEnumerable<AirlineDto>>(await airlineSearchService.SearchAirlinesAsync(searchTerm, icao, type, country, name));

        public async Task AddAirlineAsync(AirlineDto airlineDto)
        {
            var airlineEntity = mapper.Map<AirlineEntity>(airlineDto);
            await airlineRepository.AddAsync(airlineEntity);
        }

        public async Task UpdateAirlineAsync(AirlineDto airlineDto)
        {
            var airlineEntity = mapper.Map<AirlineEntity>(airlineDto);
            await airlineRepository.UpdateAsync(airlineEntity);
        }

        public async Task DeleteAirlineAsync(Guid id)
        {
            await airlineRepository.DeleteAsync(id);
        }
    }
}
