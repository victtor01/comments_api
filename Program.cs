using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using tasks_api.src.Core.Application.Services;
using tasks_api.src.Core.Interfaces.Auth;
using tasks_api.src.Core.Interfaces.Comments;
using tasks_api.src.Core.Interfaces.Jwt;
using tasks_api.src.Core.Interfaces.Users;
using tasks_api.src.Database;
using tasks_api.src.Infra.Api.Middlewares;
using tasks_api.src.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddEndpointsApiExplorer(); // add swegger
builder.Services.AddSwaggerGen(); // add swegger
builder.Services.AddControllers();

builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IJwtService, JwtService>();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();

builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
{
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder
  .Services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:secretKey"]!)),
      ClockSkew = TimeSpan.Zero,
    };
  });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<SessionMiddleware>();
app.UseMiddleware<ErrorMiddleware>();
app.UseAuthentication();
app.MapControllers();
app.Run();

// app.UseAuthorization();
// app.UseHttpsRedirection();
