using br.com.fiap.cloudgames.Domain.Repositories;

namespace br.com.fiap.cloudgames.Application.UseCases.RetrieveGame;

public class RetrieveGameUseCase
{
    private readonly IGameRepository _gameRepository;

    public RetrieveGameUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<RetrieveGameResponse> ExecuteAsync(RetrieveGameRequest request)
    {
        var parseResult = Guid.TryParse(request.GameId, out var gameId);
        if(!parseResult)
            throw new ArgumentException($"Invalid game id '{request.GameId}'");
        
        var game = await _gameRepository.GetByIdAsync(gameId);
        if(game == null)
            throw new ArgumentException($"Game with id '{request.GameId}' not found");
        
        return new RetrieveGameResponse()
        {
            Id = game.Id.ToString(),
            Title = game.Title,
            Description = game.Description,
            Story = game.Story,
            Franchise = game.Franchise,
            ReleaseDate = game.ReleaseDate,
            AgeRating = game.AgeRating.ToString(),
            GameModes = game.GameModes.Select(mode => mode.ToString()).ToList(),
            Publisher = game.Publisher.Name,
            Developers = game.Developers.Select(developer => developer.Name).ToList(),
            Platforms = game.Platforms.Select(platform => platform.ToString()).ToList(),
        };
    }
}