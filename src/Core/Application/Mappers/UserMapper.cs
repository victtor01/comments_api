using tasks_api.src.Core.Application.Dtos.User;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Core.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto { Name = userModel.Name, Age = userModel.Age };
        }
    }
}
