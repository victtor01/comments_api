using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces;
using tasks_api.src.Database;

namespace tasks_api.src.Infra.Repositories
{
    public class UsersRepository(ApplicationDatabaseContext context) : IUsersRepository
    {
        private readonly ApplicationDatabaseContext _context = context;

        public override async Task<User> Save(User user)
        {
            var entityEntry = await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return entityEntry.Entity;
        }
    }
}
