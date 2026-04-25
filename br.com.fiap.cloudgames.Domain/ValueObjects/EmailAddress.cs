using System.Net.Mail;

namespace br.com.fiap.cloudgames.Domain.ValueObjects;

public record EmailAddress
{
    public String Email { get; }

    public EmailAddress(String email)
    {
        if(String.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));
        
        if(email.Length > 254)
            throw new ArgumentOutOfRangeException(nameof(email));

        try
        {
            var validEmail = new MailAddress(email);
            Email = validEmail.Address.ToLowerInvariant();
        }
        catch (FormatException)
        {
            throw new ArgumentException("Email address must be a valid email address", nameof(email));
        }
    }
    
    public override String ToString() => Email;
}