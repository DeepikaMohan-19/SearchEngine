using AutoMapper;
using SearchEngine.Web.Application.DTOs;
using SearchEngine.Web.Application.Services.Interfaces;
using SearchEngine.Web.Domain.Entites;
using SearchEngine.Web.Domain.Interfaces.Repositories;
using SearchEngine.Web.Domain.Interfaces.Services;

namespace SearchEngine.Web.Application.Services.Implementations
{
    public class AirportsService(
        IAirportsRepository airportRepository,
        IAirportSearchService airportSearchService,
        IMapper mapper,
        ISearchHistoryRepository searchHistoryRepository) : IAirportsService
    {
        private readonly IAirportsRepository _airportRepository = airportRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<AirportDto?> GetAirportByIdAsync(Guid id)
        {
            var airportEntity = await _airportRepository.GetByIdAsync(id);
            return _mapper.Map<AirportDto>(airportEntity);
        }

        public async Task<IEnumerable<AirportDto>> GetAllAirportsAsync()
        {
            var airportEntities = await _airportRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AirportDto>>(airportEntities);
        }

        public async Task<IEnumerable<AirportDto>> SearchAirportsAsync(string? email, string searchTerm, string? city = null, string? country = null, string? iata = null, string? icao = null, double? latitude = null, double? longitude = null, int? elevation = null, string? runwayLength = null, string? type = null, string? timezone = null, string? airportType = null, string? source = null, string? name = null)
        {
            var airportEntities = await airportSearchService.SearchAirportsAsync(searchTerm, city, country, iata, icao, latitude, longitude, elevation, runwayLength, type, timezone, airportType, source, name);
            await searchHistoryRepository.AddAsync(new SearchHistoryEntity { SearchTerm = searchTerm, CreatedByEmail = email });
            return _mapper.Map<IEnumerable<AirportDto>>(airportEntities);
        }

        public async Task AddAirportAsync(AirportDto airportDto)
        {
            var airportEntity = _mapper.Map<AirportEntity>(airportDto);
            await _airportRepository.AddAsync(airportEntity);
        }

        public async Task UpdateAirportAsync(AirportDto airportDto)
        {
            var airportEntity = _mapper.Map<AirportEntity>(airportDto);
            await _airportRepository.UpdateAsync(airportEntity);
        }

        public async Task DeleteAirportAsync(Guid id)
        {
            await _airportRepository.DeleteAsync(id);
        }
    }
}