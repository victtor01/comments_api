using Microsoft.EntityFrameworkCore;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces.Comments;
using tasks_api.src.Database;

namespace tasks_api.src.Infra.Repositories
{
  public class CommentsRepository(ApplicationDatabaseContext context) : ICommentsRepository
  {
    private readonly ApplicationDatabaseContext _context = context;

    public async Task<List<Comment>> FindAll(Guid userId)
    {
      var comments = await _context
        .Comments.Where(c => c.UserId == userId)
        .Select(c => new Comment { Content = c.Content, User = c.User })
        .ToListAsync();
      return comments;
    }

    public async Task<Comment> Save(Comment comment)
    {
      var created = await _context.Comments.AddAsync(comment);
      await _context.SaveChangesAsync();

      return created.Entity;
    }
  }
}
