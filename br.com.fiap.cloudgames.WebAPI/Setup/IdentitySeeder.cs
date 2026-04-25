using br.com.fiap.cloudgames.Application.UnitsOfWork;
using br.com.fiap.cloudgames.Application.UseCases.ChangeUserRole;
using br.com.fiap.cloudgames.Application.UseCases.RegisterUser;
using br.com.fiap.cloudgames.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace br.com.fiap.cloudgames.WebAPI.Setup;

public static class IdentitySeeder
{
    public static async Task SeedRoles(IServiceProvider services, IConfiguration configuration)
    {
        if (!Boolean.Parse(configuration["EnableInitSeed"]))
            return;
        
        var unitOfWork =  services.GetRequiredService<IUnitOfWork>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = { UserRoles.admin.ToString(), UserRoles.user.ToString() };
        foreach (var role in roles)
        {
            if(!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        await unitOfWork.CommitAsync();
    }

    public static async Task SeedBootstrapUser(IServiceProvider services, IConfiguration configuration)
    {
        if (!Boolean.Parse(configuration["EnableInitSeed"]))
            return;
            
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var registerUserUseCase = services.GetRequiredService<RegisterUserUseCase>();
        var changeUserRoleUseCase = services.GetRequiredService<ChangeUserRoleUseCase>();
        
        var identityUser = await userManager.FindByEmailAsync(configuration["RootUser:Email"]);
        if (identityUser == null)
        {
            var registerUserRequest = new RegisterUserRequest()
            {
                FirstName = configuration["RootUser:FirstName"],
                LastName = configuration["RootUser:LastName"],
                Email = configuration["RootUser:Email"],
                Password = configuration["RootUser:Password"]
            };
            var resultRegisterUser = await registerUserUseCase.ExecuteAsync(registerUserRequest);

            var changeRoleRequest = new ChangeUserRoleRequest()
            {
                UserId = resultRegisterUser.Id,
                Role = UserRoles.admin.ToString(),
            };
            await changeUserRoleUseCase.ExecuteAsync(changeRoleRequest);
        }
    }
}