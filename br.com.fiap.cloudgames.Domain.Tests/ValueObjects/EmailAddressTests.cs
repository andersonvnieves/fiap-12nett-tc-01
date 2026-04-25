using br.com.fiap.cloudgames.Domain.ValueObjects;

namespace br.com.fiap.cloudgames.Domain.Tests.ValueObjects;

public class EmailAddressTests
{
    [Fact]
    public void ShouldNormalizeToLowerInvariant()
    {
        var email = new EmailAddress("Anderson.Silva@Example.com");

        Assert.Equal("anderson.silva@example.com", email.Email);
        Assert.Equal("anderson.silva@example.com", email.ToString());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void WhenEmailIsBlank_ShouldThrow(string? value)
    {
        Assert.Throws<ArgumentNullException>(() => new EmailAddress(value!));
    }

    [Fact]
    public void WhenEmailIsTooLong_ShouldThrow()
    {
        var tooLong = new string('a', 255);
        Assert.Throws<ArgumentOutOfRangeException>(() => new EmailAddress(tooLong));
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("abc@")]
    [InlineData("@example.com")]
    public void WhenEmailIsInvalid_ShouldThrow(string value)
    {
        var ex = Assert.Throws<ArgumentException>(() => new EmailAddress(value));
        Assert.Equal("email", ex.ParamName);
    }
}

