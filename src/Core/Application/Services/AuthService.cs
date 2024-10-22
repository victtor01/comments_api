using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using tasks_api.src.Core.Application.Dtos.Auth;
using tasks_api.src.Core.Application.Dtos.Jwt;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces.Auth;
using tasks_api.src.Core.Interfaces.Jwt;
using tasks_api.src.Core.Interfaces.Users;
using tasks_api.src.Infra.config;

namespace tasks_api.src.Core.Application.Services
{
  public class AuthService(IUsersRepository usersRepository, IJwtService jwtService) : IAuthService
  {
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IJwtService _jwtService = jwtService;

    public async Task<User> FindUserExists(string email)
    {
      var user = await _usersRepository.FindByEmail(email.ToLower()) ?? throw new NotFoundException("user not exists");

      return user;
    }

    public void VerifyPassword(string userId, string password, string hash)
    {
      var Indentity = new IdentityUser { Id = userId };
      var passwordHasher = new PasswordHasher<IdentityUser>();
      var hashed = passwordHasher.VerifyHashedPassword(Indentity, hash, password);

      if (hashed == PasswordVerificationResult.Failed)
        throw new UnauthorizedAccessException("Email ou senha incorretos!");
    }

    public async Task<JwtDto> Auth(AuthDto authDto)
    {
      var user = await FindUserExists(authDto.Email);

      string hash = user.Password;
      string password = authDto.Password;
      string userId = user.Id.ToString();
      string email = user.Email.ToLower();
      VerifyPassword(userId, password, hash);

      JwtDto tokens = _jwtService.CreateJwtToken(userId, email);

      return tokens;
    }
  }
}
