using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using tasks_api.src.Core.Application.Dtos.Jwt;

namespace tasks_api.src.Core.Interfaces.Jwt
{
  public interface IJwtService
  {
    public JwtDto CreateJwtToken(string userId, string email);
    public Dictionary<string, string> GetClaimsOfJwtSecurityToken(JwtSecurityToken jwtSecurityToken);
    public JwtSecurityToken DecodeJwt(string token);
    public Dictionary<string, string> DecodeTokenAndGetClaims(string token);
  }
}
