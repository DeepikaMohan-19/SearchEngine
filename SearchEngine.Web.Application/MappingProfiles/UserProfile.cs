using AutoMapper;
using SearchEngine.Web.Application.DTOs.Authentication;
using SearchEngine.Web.Domain.Entites;

namespace SearchEngine.Web.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>();
        }
    }
}
