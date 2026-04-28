using br.com.fiap.cloudgames.Application.UseCases.User.ChangeUserRole;
using br.com.fiap.cloudgames.Application.UseCases.Game.CreateGame;
using br.com.fiap.cloudgames.Application.UseCases.Game.UpdateGame;
using br.com.fiap.cloudgames.Application.UseCases.User.LogIn;
using br.com.fiap.cloudgames.Application.UseCases.User.RegisterUser;

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

    public static UpdateGameRequest ValidUpdateGameRequest() => new()
    {
        Id = Guid.NewGuid().ToString(),
        Title = "My Game Updated",
        Description = "A cool game - updated",
        Story = "Once upon a time - updated",
        Franchise = "Franchise X",
        ReleaseDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
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

