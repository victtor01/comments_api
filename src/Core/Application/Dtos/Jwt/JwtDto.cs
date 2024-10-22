using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tasks_api.src.Core.Application.Dtos.Jwt
{
  public class JwtDto(string accessToken, string refreshToken)
  {
    public string AccessToken { get; set; } = accessToken;
    public string RefreshToken { get; set; } = refreshToken;
  }
}
