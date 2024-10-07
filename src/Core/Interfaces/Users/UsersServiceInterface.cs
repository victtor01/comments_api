using tasks_api.src.Core.Application.Dtos.User;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Core.Interfaces.Users
{
  public abstract class IUsersService()
  {
    public abstract Task<User> Create(UserDto userDto);
  }
}
