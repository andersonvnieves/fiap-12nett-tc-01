using br.com.fiap.cloudgames.Application.UseCases.ChangeUserRole;
using br.com.fiap.cloudgames.Application.UseCases.CreateGame;
using br.com.fiap.cloudgames.Application.UseCases.LogIn;
using br.com.fiap.cloudgames.Application.UseCases.RegisterUser;

namespace br.com.fiap.cloudgames.Application.Tests.TestData;

public static class ApplicationTestData
{
    public static CreateGameRequest ValidCreateGameRequest() => new()
    {
        Title = "My Game",
        Description = "A cool game",
        Story = "Once upon a time",
        Franchise = "Franchise X",
        ReleaseDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
        AgeRating = "LIVRE",
        GameModes = ["SinglePlayer"],
        Publisher = "Publisher Inc",
        Developers = ["Dev Studio"],
        Platforms = ["Windows"]
    };

    public static RegisterUserRequest ValidRegisterUserRequest() => new()
    {
        FirstName = "Anderson",
        LastName = "Silva",
        Email = "anderson.silva@example.com",
        Password = "Pass@123!"
    };

    public static LogInRequest ValidLogInRequest() => new()
    {
        email = "anderson.silva@example.com",
        password = "Pass@123!"
    };

    public static ChangeUserRoleRequest ValidChangeUserRoleRequest() => new()
    {
        UserId = Guid.NewGuid().ToString(),
        Role = "admin"
    };
}

