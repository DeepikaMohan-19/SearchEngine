using AutoMapper;
using SearchEngine.Web.Application.DTOs;
using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Application.MappingProfiles
{
    public class SearchHistoryProfile : Profile
    {
        public SearchHistoryProfile()
        {
            CreateMap<SearchHistoryEntity, SearchHistoryDto>();
            CreateMap<SearchHistoryDto, SearchHistoryEntity>();
        }
    }
}
