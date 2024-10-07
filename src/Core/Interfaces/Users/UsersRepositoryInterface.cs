using System.ComponentModel.DataAnnotations;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Core.Interfaces.Users
{
  public interface IUsersRepository
  {
    Task<User> FindByEmail(string email);
    Task<User> Save(User user);
  }
}
