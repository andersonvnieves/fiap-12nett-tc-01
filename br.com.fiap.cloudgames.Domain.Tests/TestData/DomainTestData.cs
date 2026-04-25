using br.com.fiap.cloudgames.Domain.Entities;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Domain.ValueObjects;

namespace br.com.fiap.cloudgames.Domain.Tests.TestData;

public static class DomainTestData
{
    public static Name ValidName() => new("Anderson", "Silva");

    public static EmailAddress ValidEmail() => new("Anderson.Silva@Example.com");

    public static string ValidIdentityId() => "identity-123";

    public static Publisher ValidPublisher() => new("Publisher Inc");

    public static List<Developer> ValidDevelopers() => [new Developer("Dev Studio")];

    public static List<GameModes> ValidGameModes() => [GameModes.SinglePlayer];

    public static List<Platforms> ValidPlatforms() => [Platforms.Windows];
}

