namespace tasks_api.src.Core.Application.Dtos.User
{
  public class UserDto
  {
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
  }
}
