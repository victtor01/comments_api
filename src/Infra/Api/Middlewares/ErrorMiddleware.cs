using System.Net;
using System.Text.Json;
using tasks_api.src.Infra.config;

namespace tasks_api.src.Infra.Api.Middlewares
{
  public class ErrorMiddleware(RequestDelegate next)
  {
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await HandleExceptionAsync(context, ex);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      var response = context.Response;
      response.ContentType = "application/json";

      if (exception is ErrorInstance errorInstance)
      {
        response.StatusCode = errorInstance.StatusCode;
        var result = JsonSerializer.Serialize(new { error = errorInstance.Error, message = errorInstance.Message });
        return response.WriteAsync(result);
      }
      else
      {
        // Para exceções não tratadas
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var defaultResult = JsonSerializer.Serialize(
          new { error = "Internal Server Error", message = exception.Message }
        );
        return response.WriteAsync(defaultResult);
      }
    }
  }
}
