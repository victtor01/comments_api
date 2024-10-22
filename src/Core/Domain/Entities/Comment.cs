using System.ComponentModel.DataAnnotations;

namespace tasks_api.src.Core.Domain.Entities
{
  public class Comment
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
  }
}
