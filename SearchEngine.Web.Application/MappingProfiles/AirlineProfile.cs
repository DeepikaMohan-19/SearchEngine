using AutoMapper;
using SearchEngine.Web.Application.DTOs;
using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Application.MappingProfiles
{
    public class AirlineProfile : Profile
    {
        public AirlineProfile()
        {
            CreateMap<AirlineEntity, AirlineDto>();
            CreateMap<AirlineDto, AirlineEntity>();
        }
    }
}
