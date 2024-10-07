using Microsoft.EntityFrameworkCore;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces.Users;
using tasks_api.src.Database;

namespace tasks_api.src.Infra.Repositories
{
  public class UsersRepository(ApplicationDatabaseContext context) : IUsersRepository
  {
    private readonly ApplicationDatabaseContext _context = context;

    public async Task<User> Save(User user)
    {
      var entityEntry = await _context.AddAsync(user);
      await _context.SaveChangesAsync();

      return entityEntry.Entity;
    }

    public async Task<User> FindByEmail(string email)
    {
      User user = await _context.User.SingleAsync(user => user.Email == email);

      return user;
    }
  }
}
