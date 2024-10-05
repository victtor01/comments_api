using Microsoft.EntityFrameworkCore;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Database
{
    public class ApplicationDatabaseContext(DbContextOptions dbContextOptions)
        : DbContext(dbContextOptions)
    {
        public DbSet<User> User { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}
