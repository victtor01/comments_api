using Microsoft.EntityFrameworkCore;
using tasks_api.src.Core.Application.Services;
using tasks_api.src.Core.Interfaces;
using tasks_api.src.Database;
using tasks_api.src.Infra.Api.Middlewares;
using tasks_api.src.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
{
  options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ErrorMiddleware));

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
