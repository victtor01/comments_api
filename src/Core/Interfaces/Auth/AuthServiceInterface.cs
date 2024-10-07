namespace tasks_api.src.Core.Interfaces.Auth
{
  public interface IAuthService
  {
    Task<bool> Auth(string email, string password);
  }
}
