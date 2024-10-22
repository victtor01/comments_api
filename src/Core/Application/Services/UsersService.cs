using tasks_api.src.Core.Application.Dtos.User;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces.Users;
using tasks_api.src.Infra.config;

namespace tasks_api.src.Core.Application.Services
{
  public class UsersService(IUsersRepository usersRepository) : IUsersService
  {
    private readonly IUsersRepository _usersRepository = usersRepository;

    public async Task<User> Create(UserDto userDto)
    {
      var userInDatabase = await _usersRepository.FindByEmail(userDto.Email);

      if (userInDatabase != null)
        throw new BadRequestException("User exists");

      var user = new User
      {
        Age = userDto.Age,
        Name = userDto.Name.ToLower(),
        Email = userDto.Email.ToLower(),
      };

      string userId = user.Id.ToString();

      user.HashAndSetPassword(userId, userDto.Password);
      user.IsValidEmail();

      var created = await _usersRepository.Save(user);

      return created;
    }
  }
}
