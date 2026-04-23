namespace br.com.fiap.cloudgames.Domain.Entities;

public class Developer
{
    public String Name { get; }
    
    public Developer(String name)
    {
        Name = String.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }
}