namespace br.com.fiap.cloudgames.Domain.Entities;

public record Publisher
{
    public String Name { get; }

    public Publisher(String name)
    {
        Name = String.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name;
    }
}