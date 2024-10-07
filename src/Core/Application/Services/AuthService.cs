using tasks_api.src.Core.Interfaces.Users;

namespace tasks_api.src.Core.Application.Services
{
  public class AuthService(IUsersRepository usersRepository)
  {
    private readonly IUsersRepository _usersRepository = usersRepository;
  }
}
