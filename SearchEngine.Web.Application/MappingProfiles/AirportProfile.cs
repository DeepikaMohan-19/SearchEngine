using AutoMapper;
using SearchEngine.Web.Application.DTOs;
using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Application.MappingProfiles
{
    public class AirportProfile : Profile
    {
        public AirportProfile()
        {
            CreateMap<AirportEntity, AirportDto>();
            CreateMap<AirportDto, AirportEntity>();
        }
    }
}
