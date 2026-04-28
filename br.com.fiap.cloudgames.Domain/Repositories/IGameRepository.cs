using br.com.fiap.cloudgames.Domain.Aggregates;

namespace br.com.fiap.cloudgames.Domain.Repositories;

public interface IGameRepository
{
    Task AddAsync(Game game);
    Task<Game?> GetByIdAsync(Guid id);
    void Update(Game game);
}