namespace br.com.fiap.cloudgames.Domain.ValueObjects;

public record Name
{
    public String FirstName { get; }
    public String LastName { get; }
    
    public String FullName  => $"{FirstName} {LastName}";

    public Name(String firstName, String lastName)
    {
        if(String.IsNullOrWhiteSpace(firstName))
            throw new ArgumentNullException(nameof(firstName));
        
        if(String.IsNullOrWhiteSpace(lastName))
            throw new ArgumentNullException(nameof(lastName));
        
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }       
}