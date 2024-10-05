using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Core.Interfaces
{
    public abstract class IUsersRepository
    {
        public abstract Task<User> Save(User user);
    }
}
