using SearchEngine.Web.Application.DTOs.Authentication;

namespace SearchEngine.Web.Application.Authentication
{
    public interface IJwtAuthenticationService
    {
        string GenerateJwtToken(UserDto user);
    }
}
