using Microsoft.AspNetCore.Mvc;
using tasks_api.src.Core.Application.Dtos.Auth;
using tasks_api.src.Core.Application.Dtos.Jwt;
using tasks_api.src.Core.Interfaces.Auth;
using tasks_api.src.Infra.config;

namespace tasks_api.src.Infra.Api.Controllers
{
  [Route("/auth")]
  [ApiController]
  public class AuthController(IAuthService authService) : ControllerBase
  {
    private readonly IAuthService _authService = authService;

    [HttpPost]
    public async Task<IActionResult> Auth([FromBody] AuthDto authDto)
    {
      JwtDto tokens = await _authService.Auth(authDto);
      var cookiesNamesFields = new CookiesFields();

      var cookiesOptions = new CookieOptions { HttpOnly = true };
      Response.Cookies.Append(cookiesNamesFields.AccessToken, tokens.AccessToken, cookiesOptions);
      Response.Cookies.Append(cookiesNamesFields.RefreshToken, tokens.RefreshToken, cookiesOptions);

      return Ok(tokens);
    }
  }
}
