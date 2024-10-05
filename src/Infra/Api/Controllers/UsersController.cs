using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasks_api.src.Core.Application.Dtos.User;
using tasks_api.src.Core.Application.Mappers;
using tasks_api.src.Core.Domain.Entities;
using tasks_api.src.Core.Interfaces;
using tasks_api.src.Database;

namespace tasks_api.src.Infra.Api.Controllers
{
  [Route("users")]
  [ApiController]
  public class UsersController(ApplicationDatabaseContext context, IUsersService usersService) : ControllerBase
  {
    private readonly ApplicationDatabaseContext _context = context;
    private readonly IUsersService _usersService = usersService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      List<User> users = await _context.User.ToListAsync();
      var usersMappers = users.Select(user => user.ToUserDto());

      return Ok(usersMappers);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
      var created = await _usersService.Create(userDto);

      return Ok(created);
    }
  }
}
