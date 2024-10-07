using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using tasks_api.src.Infra.config;

namespace tasks_api.src.Core.Domain.Entities
{
  public class User
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    private int _age;
    private string _password = string.Empty;
    public List<Comment> Comments { get; set; } = [];

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Password
    {
      get => _password;
      set => _password = value.Length < 5 ? throw new BadHttpRequestException("Senha curta demais!") : value;
    }

    [Required]
    public int Age
    {
      get => _age;
      set => _age = value < 16 || value > 100 ? throw new BadRequestException("Idade inválida") : value;
    }

    public void IsValidEmail()
    {
      if (string.IsNullOrEmpty(Email) || Email.Length < 10)
        throw new BadHttpRequestException("Email inválido");

      try
      {
        MailAddress mailAddress;
        mailAddress = new MailAddress(Email);
      }
      catch (Exception ex)
      {
        throw new BadHttpRequestException(ex.Message);
      }
    }
  }
}
