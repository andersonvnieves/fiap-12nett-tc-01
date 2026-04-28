using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Exceptions;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Domain.Tests.TestData;

namespace br.com.fiap.cloudgames.Domain.Tests.Aggregates;

public class GameTests
{
    [Fact]
    public void CreateGame_ShouldCreateGame_WithValidData()
    {
        var game = Game.CreateGame(
            title: "My Game",
            description: "A cool game",
            story: "Once upon a time",
            franchise: "Franchise X",
            releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
            ageRating: AgeRating.LIVRE,
            gameModes: DomainTestData.ValidGameModes(),
            publisher: DomainTestData.ValidPublisher(),
            developers: DomainTestData.ValidDevelopers(),
            platforms: DomainTestData.ValidPlatforms());

        Assert.NotEqual(Guid.Empty, game.Id);
        Assert.Equal("My Game", game.Title);
        Assert.Equal("A cool game", game.Description);
        Assert.Equal("Once upon a time", game.Story);
        Assert.Equal("Franchise X", game.Franchise);
        Assert.Equal(AgeRating.LIVRE, game.AgeRating);
        Assert.NotNull(game.Publisher);
        Assert.NotEmpty(game.GameModes);
        Assert.NotEmpty(game.Developers);
        Assert.NotEmpty(game.Platforms);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateGame_WhenTitleIsBlank_ShouldThrow(string? title)
    {
        var ex = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: title!,
                description: "desc",
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: DomainTestData.ValidGameModes(),
                publisher: DomainTestData.ValidPublisher(),
                developers: DomainTestData.ValidDevelopers(),
                platforms: DomainTestData.ValidPlatforms()));
        Assert.Contains("Title is required.", ex.Errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateGame_WhenDescriptionIsBlank_ShouldThrow(string? description)
    {
        var ex = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: description!,
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: DomainTestData.ValidGameModes(),
                publisher: DomainTestData.ValidPublisher(),
                developers: DomainTestData.ValidDevelopers(),
                platforms: DomainTestData.ValidPlatforms()));
        Assert.Contains("Description is required.", ex.Errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void CreateGame_WhenStoryIsBlank_ShouldThrow(string? story)
    {
        var ex = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: "desc",
                story: story!,
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: DomainTestData.ValidGameModes(),
                publisher: DomainTestData.ValidPublisher(),
                developers: DomainTestData.ValidDevelopers(),
                platforms: DomainTestData.ValidPlatforms()));
        Assert.Contains("Story is required.", ex.Errors);
    }

    [Fact]
    public void CreateGame_WhenReleaseDateIsInTheFuture_ShouldThrow()
    {
        var ex = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: "desc",
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                ageRating: AgeRating.LIVRE,
                gameModes: DomainTestData.ValidGameModes(),
                publisher: DomainTestData.ValidPublisher(),
                developers: DomainTestData.ValidDevelopers(),
                platforms: DomainTestData.ValidPlatforms()));
        Assert.Contains("ReleaseDate cannot be in the future.", ex.Errors);
    }

    [Fact]
    public void CreateGame_WhenGameModesIsNullOrEmpty_ShouldThrow()
    {
        var ex1 = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: "desc",
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: null!,
                publisher: DomainTestData.ValidPublisher(),
                developers: DomainTestData.ValidDevelopers(),
                platforms: DomainTestData.ValidPlatforms()));
        Assert.Contains("At least one GameMode is required.", ex1.Errors);

        var ex2 = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: "desc",
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: [],
                publisher: DomainTestData.ValidPublisher(),
                developers: DomainTestData.ValidDevelopers(),
                platforms: DomainTestData.ValidPlatforms()));
        Assert.Contains("At least one GameMode is required.", ex2.Errors);
    }

    [Fact]
    public void CreateGame_WhenDevelopersIsNullOrEmpty_ShouldThrow()
    {
        var ex1 = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: "desc",
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: DomainTestData.ValidGameModes(),
                publisher: DomainTestData.ValidPublisher(),
                developers: null!,
                platforms: DomainTestData.ValidPlatforms()));
        Assert.Contains("At least one Developer is required.", ex1.Errors);

        var ex2 = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: "desc",
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: DomainTestData.ValidGameModes(),
                publisher: DomainTestData.ValidPublisher(),
                developers: [],
                platforms: DomainTestData.ValidPlatforms()));
        Assert.Contains("At least one Developer is required.", ex2.Errors);
    }

    [Fact]
    public void CreateGame_WhenPlatformsIsNullOrEmpty_ShouldThrow()
    {
        var ex1 = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: "desc",
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: DomainTestData.ValidGameModes(),
                publisher: DomainTestData.ValidPublisher(),
                developers: DomainTestData.ValidDevelopers(),
                platforms: null!));
        Assert.Contains("At least one Platform is required.", ex1.Errors);

        var ex2 = Assert.Throws<DomainException>(() =>
            Game.CreateGame(
                title: "title",
                description: "desc",
                story: "story",
                franchise: "franchise",
                releaseDate: DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
                ageRating: AgeRating.LIVRE,
                gameModes: DomainTestData.ValidGameModes(),
                publisher: DomainTestData.ValidPublisher(),
                developers: DomainTestData.ValidDevelopers(),
                platforms: []));
        Assert.Contains("At least one Platform is required.", ex2.Errors);
    }
}

