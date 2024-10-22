using Microsoft.AspNetCore.Mvc;
using tasks_api.src.Core.Application.Dtos.Comments;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces.Comments;
using tasks_api.src.Infra.Extensions;

namespace tasks_api.src.Infra.Api.Controllers
{
  [Route("/comments")]
  public class CommentsController(ICommentsService commentsService) : ControllerBase
  {
    private readonly ICommentsService _commentsService = commentsService;

    [HttpPost()]
    public async Task<IActionResult> Create([FromBody] CommentDto commentDto)
    {
      var session = HttpContext.GetSession();
      Console.WriteLine(commentDto.Content);
      var created = await _commentsService.Create(commentDto, session.UserId);

      return Ok(created);
    }

    [HttpGet]
    public async Task<ActionResult<List<Comment>>> FindAll()
    {
      Session session = HttpContext.GetSession();
      var comments = await _commentsService.FindAll(session.UserId);

      return Ok(comments);
    }
  }
}
