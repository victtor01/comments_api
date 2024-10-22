using tasks_api.src.Core.Application.Dtos.Comments;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces.Comments;
using tasks_api.src.Infra.config;
using tasks_api.src.Infra.Repositories;

namespace tasks_api.src.Core.Application.Services
{
  public class CommentsService(ICommentsRepository commentsRepository) : ICommentsService
  {
    private readonly ICommentsRepository _commentsRepository = commentsRepository;

    public async Task<Comment> Create(CommentDto commentDto, Guid userId)
    {
      try
      {
        Comment comment = new() { Content = commentDto.Content, UserId = userId };
        Comment created = await _commentsRepository.Save(comment);

        return created;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw new BadRequestException("Houve um erro ao tentar criar um novo comentário");
      }
    }

    public async Task<List<Comment>> FindAll(Guid userId)
    {
      var comments = await _commentsRepository.FindAll(userId);
      return comments;
    }
  }
}
