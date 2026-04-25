using br.com.fiap.cloudgames.Application.UseCases.RegisterUser;
using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Domain.UnitsOfWork;
using br.com.fiap.cloudgames.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace br.com.fiap.cloudgames.WebAPI.Setup;

public static class IdentitySeeder
{
    public static async Task SeedRoles(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //TODO: Param ?
        string[] roles = { "admin", "user" };
        foreach (var role in roles)
        {
            if(!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    public static async Task SeedBootstrapUser(IServiceProvider services, IConfiguration configuration)
    {
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var userRepository = services.GetRequiredService<IUserRepository>();
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();
        
        var name = new Name(configuration["RootUser:FirstName"],  configuration["RootUser:LastName"]);
        var email = new EmailAddress(configuration["RootUser:Email"]);
        var password = configuration["RootUser:Password"];
        var identityUder = await userManager.FindByEmailAsync(email.Email);
        
        await unitOfWork.BeginTransactionAsync();
        if (identityUder == null)
        {
            try
            {
                identityUder = new IdentityUser
                {
                    Email = email.Email,
                    UserName = email.Email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(identityUder, password);
                if (!result.Succeeded)
                    throw new Exception("Erro ao criar root user");
                await userManager.AddToRoleAsync(identityUder, "Admin");
            
                var user = User.Create(name, email, identityUder.Id);
                await userRepository.AddAsync(user);
                await unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
            }
        }
    }
}