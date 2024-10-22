using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using tasks_api.src.Core.Application.Dtos.Jwt;
using tasks_api.src.Core.Interfaces.Jwt;

namespace tasks_api.src.Core.Application.Services
{
  public class JwtService(IConfiguration configuration) : IJwtService
  {
    private readonly IConfiguration _configuration = configuration;

    public Dictionary<string, string> GetClaimsOfJwtSecurityToken(JwtSecurityToken jwtSecurityToken) =>
      jwtSecurityToken.Claims.ToDictionary(c => c.Type, c => c.Value);

    public JwtSecurityToken DecodeJwt(string token)
    {
      var handler = new JwtSecurityTokenHandler();
      JwtSecurityToken decodedToken = handler.ReadJwtToken(token);
      return decodedToken;
    }

    public Dictionary<string, string> DecodeTokenAndGetClaims(string token)
    {
      JwtSecurityToken securityToken = DecodeJwt(token);
      Dictionary<string, string> Claims = GetClaimsOfJwtSecurityToken(securityToken);
      return Claims;
    }

    public (string, string) GenerateToken(Claim[] claims)
    {
      var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]!));
      var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

      DateTime expiration = DateTime.UtcNow.AddMinutes(1);
      DateTime longerExpiration = DateTime.UtcNow.AddMinutes(10);

      var token = new JwtSecurityToken(expires: expiration, signingCredentials: credentials, claims: claims);

      var refreshToken = new JwtSecurityToken(
        expires: longerExpiration,
        signingCredentials: credentials,
        claims: claims
      );

      string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
      string refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

      return (tokenString, refreshTokenString);
    }

    public JwtDto CreateJwtToken(string userId, string email)
    {
      var claims = new Claim[]
      {
        new("userId", userId),
        new("email", email),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      };

      var (tokenString, refreshTokenString) = GenerateToken(claims);
      JwtDto jwtDto = new(accessToken: tokenString, refreshToken: refreshTokenString);

      return jwtDto;
    }
  }
}
