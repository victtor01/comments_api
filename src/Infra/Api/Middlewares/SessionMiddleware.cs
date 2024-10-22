using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces.Jwt;
using tasks_api.src.Infra.Attributes;
using tasks_api.src.Infra.config;
using tasks_api.src.Infra.Extensions;

namespace tasks_api.src.Infra.Api.Middlewares
{
  public class SessionMiddleware(RequestDelegate next, IJwtService jwtService)
  {
    private readonly RequestDelegate _next = next;
    private readonly IJwtService _jwtService = jwtService;

    public bool IsPublicRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      var isPublicRoute = endpoint?.Metadata?.GetMetadata<PublicRoute>() != null;
      return isPublicRoute;
    }

    public string GetCookieToken(HttpContext context)
    {
      var fieldsNameSession = new CookiesFields();
      var cookiesAccessToken = context.Request.Cookies[fieldsNameSession.AccessToken] ?? null;
      if (string.IsNullOrEmpty(cookiesAccessToken))
        throw new UnauthorizedAccessException("Faça o login novamente!");

      return cookiesAccessToken;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      if (IsPublicRoute(httpContext))
      {
        await _next(httpContext);
        return;
      }

      string cookiesAccessToken = GetCookieToken(httpContext);
      var payload = _jwtService.DecodeTokenAndGetClaims(cookiesAccessToken);

      var createSession = new Session(userId: Guid.Parse(payload["userId"]), email: payload["email"]);
      httpContext.SetSession(createSession);

      await _next(httpContext);
    }
  }
}

// Console.WriteLine(JsonConvert.SerializeObject(payload, Formatting.Indented));
