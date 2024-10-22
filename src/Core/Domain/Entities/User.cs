using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using tasks_api.src.Infra.config;

namespace tasks_api.src.Core.Domain.Entities
{
  public class User
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    private int _age;

    private string _password = string.Empty;

    public List<Comment>? Comments { get; set; } = [];

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Password
    {
      get => _password;
      private set => _password = value;
    }

    [Required]
    public int Age
    {
      get => _age;
      set => _age = value < 16 || value > 100 ? throw new BadRequestException("Idade inválida") : value;
    }

    public void IsValidEmail()
    {
      if (string.IsNullOrEmpty(Email) || Email.Length < 5)
        throw new BadHttpRequestException("Email invalido");

      try
      {
        MailAddress mailAddress;
        mailAddress = new MailAddress(Email);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        throw new BadHttpRequestException("O email é invalido");
      }
    }

    public void HashAndSetPassword(string userId, string password)
    {
      if (password.Length < 6)
        throw new BadHttpRequestException("Senha curta demais");

      var passwordHasher = new PasswordHasher<string>();
      string newPassword = passwordHasher.HashPassword(userId, password);
      _password = newPassword;
    }
  }
}
