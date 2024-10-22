using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Infra.Extensions
{
  public static class HttpContextExtensions
  {
    private const string SessionKey = "session";

    public static Session GetSession(this HttpContext context)
    {
      if (context.Items[SessionKey] is Session session)
        return session;

      throw new UnauthorizedAccessException("A sessão pode estar expirada, tente fazer o login novamente!");
    }

    public static void SetSession(this HttpContext context, Session session)
    {
      context.Items[SessionKey] = session;
    }
  }
}
