using br.com.fiap.cloudgames.Domain.Aggregates;

namespace br.com.fiap.cloudgames.Domain.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);

}