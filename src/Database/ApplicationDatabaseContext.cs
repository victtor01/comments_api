using Microsoft.EntityFrameworkCore;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Database
{
  public class ApplicationDatabaseContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
  {
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Comment>(entity =>
      {
        entity.Property(e => e.Id).HasColumnType("uuid");
        entity.HasKey(e => e.Id);
        entity.HasOne(e => e.User).WithMany(u => u.Comments).HasForeignKey(e => e.UserId);
      });

      modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
      modelBuilder.Entity<User>(entity =>
      {
        entity.Property(e => e.Id).HasColumnType("uuid");
        entity.HasKey(e => e.Id);
        entity.HasMany(u => u.Comments).WithOne(c => c.User).HasForeignKey(c => c.UserId);
      });
    }
  }
}
