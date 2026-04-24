using br.com.fiap.cloudgames.Application.UseCases.CreateGame;
using br.com.fiap.cloudgames.Application.UseCases.RegisterUser;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Infrastructure.Persistence.Context;
using br.com.fiap.cloudgames.Infrastructure.Persistence.Repositories;
using br.com.fiap.cloudgames.WebAPI.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add Db Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Scoped );

//Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(option =>
    {
        option.Password.RequireUppercase = true;
        option.Password.RequireLowercase = true;
        option.Password.RequireDigit = true;
        option.Password.RequireNonAlphanumeric = true;
        option.Password.RequiredLength = 8;
        option.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

//Authentication
builder.Services.AddAuthentication()
    .AddJwtBearer();

//Authorization
builder.Services.AddAuthorization();

//Repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//UseCases
builder.Services.AddScoped<CreateGameUseCase>();
builder.Services.AddScoped<RegisterUserUseCase>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "FIAP Cloud Games (FCG)",
        Version = "v1",
        Description = "API de jogos e usuários"
    });
});

var app = builder.Build();

app.UseRequestLoggingMiddleware();
app.UseErrorHandlingMiddleware();

//Map Identity Endpoints
//app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();