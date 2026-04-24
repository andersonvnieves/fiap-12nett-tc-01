using System.Security.AccessControl;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Domain.ValueObjects;

namespace br.com.fiap.cloudgames.Domain.Aggregates;

public class User
{
    public User() { } //ORM

    #region  FactoryMethod
    public static User Create(Name name, EmailAddress email, String  identityId)
    {
        Validate(name, email, identityId);
        return new User()
        {
            Id = Guid.NewGuid(),
            Name = name, 
            Email = email,
            UserAccountStatus = UserAccountStatus.PendingVerification,
            IdentityId = identityId,
            CreationDate = DateTime.Now
        };
    }
    #endregion
    
    #region  Properties
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public UserAccountStatus UserAccountStatus { get; private set; }
    public String IdentityId { get; set; }
    public DateTime CreationDate { get; set; }
    #endregion

    private static void Validate(Name name, EmailAddress email, String identityId)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        if (email is null)
            throw new ArgumentNullException(nameof(email));
        if (String.IsNullOrEmpty(identityId))
            throw new ArgumentNullException(nameof(identityId));
    }
    
}