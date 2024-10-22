using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;

namespace tasks_api.src.Infra.config
{
  public class CookiesFields
  {
    private static readonly string _accessToken = "_accessToken";
    private static readonly string _refreshToken = "_refreshToken";

    public string AccessToken
    {
      get => _accessToken;
    }

    public string RefreshToken
    {
      get => _refreshToken;
    }
  }
}
