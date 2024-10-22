using tasks_api.src.Core.Application.Dtos.Comments;
using tasks_api.src.Core.Domain.Entities;

namespace tasks_api.src.Core.Interfaces.Comments
{
  public interface ICommentsService
  {
    public Task<Comment> Create(CommentDto commentDto, Guid userId);
    public Task<List<Comment>> FindAll(Guid userId);
  }
}
