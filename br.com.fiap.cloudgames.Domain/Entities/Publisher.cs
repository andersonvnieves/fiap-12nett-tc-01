using br.com.fiap.cloudgames.Domain.Exceptions;

namespace br.com.fiap.cloudgames.Domain.Entities;

public record Publisher
{
    public String Name { get; }

    public Publisher(String name)
    {
        var errors = new List<string>();

        if (String.IsNullOrWhiteSpace(name))
            errors.Add("Publisher name is required.");

        if (errors.Any())
            throw new DomainException(errors);

        Name = name.Trim();
    }
}