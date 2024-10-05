using System.ComponentModel.DataAnnotations.Schema;
using tasks_api.src.Infra.config;

namespace tasks_api.src.Core.Domain.Entities
{
  public class User
  {
    private int _age;

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public int Age
    {
      get => _age;
      set => _age = value < 18 ? throw new BadRequestException("Idade inválida") : value;
    }

    public List<Comment> Comments { get; set; } = [];
  }
}
