using tasks_api.src.Core.Application.Dtos.Auth;
using tasks_api.src.Core.Application.Dtos.Jwt;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Core.Interfaces.Auth
{
  public interface IAuthService
  {
    Task<JwtDto> Auth(AuthDto authDto);
    Task<User> FindUserExists(string email);
    void VerifyPassword(string userId, string password, string hash);
  }
}
