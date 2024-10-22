namespace tasks_api.src.Core.Domain.Entities
{
  public class Session(Guid userId, string email)
  {
    public Guid UserId { get; set; } = userId;
    public string Email { get; set; } = email;
  }
}
