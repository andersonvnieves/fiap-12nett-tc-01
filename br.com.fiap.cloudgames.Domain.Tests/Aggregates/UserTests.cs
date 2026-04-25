using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Domain.Tests.TestData;

namespace br.com.fiap.cloudgames.Domain.Tests.Aggregates;

public class UserTests
{
    [Fact]
    public void Create_ShouldCreateActiveUser_WithDefaultRoleAndCreationDate()
    {
        var name = DomainTestData.ValidName();
        var email = DomainTestData.ValidEmail();
        var identityId = DomainTestData.ValidIdentityId();

        var user = User.Create(name, email, identityId);

        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(UserAccountStatus.ACTIVE, user.UserAccountStatus);
        Assert.Equal(UserRoles.user, user.Role);
        Assert.Equal(identityId, user.IdentityId);
        Assert.True(user.CreationDate <= DateTime.Now);
    }

    [Fact]
    public void Create_WhenNameIsNull_ShouldThrow()
    {
        var email = DomainTestData.ValidEmail();
        var identityId = DomainTestData.ValidIdentityId();

        Assert.Throws<ArgumentNullException>(() => User.Create(null!, email, identityId));
    }

    [Fact]
    public void Create_WhenEmailIsNull_ShouldThrow()
    {
        var name = DomainTestData.ValidName();
        var identityId = DomainTestData.ValidIdentityId();

        Assert.Throws<ArgumentNullException>(() => User.Create(name, null!, identityId));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_WhenIdentityIdIsNullOrEmpty_ShouldThrow(string? identityId)
    {
        var name = DomainTestData.ValidName();
        var email = DomainTestData.ValidEmail();

        Assert.Throws<ArgumentNullException>(() => User.Create(name, email, identityId!));
    }
}

