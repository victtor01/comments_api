using tasks_api.src.Core.Application.Dtos.User;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces;

namespace tasks_api.src.Core.Application.Services
{
  public class UsersService(IUsersRepository usersRepository) : IUsersService
  {
    private readonly IUsersRepository _usersRepository = usersRepository;

    public override async Task<User> Create(UserDto userDto)
    {
      var user = new User { Name = userDto.Name, Age = userDto.Age };

      var created = await _usersRepository.Save(user);

      return created;
    }
  }
}
