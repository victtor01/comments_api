using tasks_api.src.Core.Application.Dtos.User;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Core.Application.Mappers
{
  public static class UserMapper
  {
    public static UserDtoMapper ToUserDto(this User userModel)
    {
      return new UserDtoMapper { Name = userModel.Name, Age = userModel.Age };
    }
  }

  public class UserDtoMapper
  {
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public string Email { get; set; } = string.Empty;
  }
}
