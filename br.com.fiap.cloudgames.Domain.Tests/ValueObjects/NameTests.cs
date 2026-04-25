using br.com.fiap.cloudgames.Domain.ValueObjects;

namespace br.com.fiap.cloudgames.Domain.Tests.ValueObjects;

public class NameTests
{
    [Fact]
    public void ShouldTrimNames_AndBuildFullName()
    {
        var name = new Name("  Anderson ", " Silva  ");

        Assert.Equal("Anderson", name.FirstName);
        Assert.Equal("Silva", name.LastName);
        Assert.Equal("Anderson Silva", name.FullName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void WhenFirstNameIsBlank_ShouldThrow(string? firstName)
    {
        Assert.Throws<ArgumentNullException>(() => new Name(firstName!, "Silva"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void WhenLastNameIsBlank_ShouldThrow(string? lastName)
    {
        Assert.Throws<ArgumentNullException>(() => new Name("Anderson", lastName!));
    }
}

