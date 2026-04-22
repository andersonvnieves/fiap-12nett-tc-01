using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Entities;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Domain.Repositories;

namespace br.com.fiap.cloudgames.Application.UseCases.CreateGame;

public class CreateGameUseCase
{
    //TODO: Add logger

    private readonly IGameRepository  _gameRepository;
    
    public CreateGameUseCase(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository; 
    }

    public async Task<CreateGameResponse> ExecuteAsync(CreateGameRequest request)
    {
        if (!Enum.TryParse<AgeRating>(request.AgeRating, true, out var ageRating))
            throw new ArgumentException($"Invalid age rating '{request.AgeRating}'");
        
        var gameModes = new List<GameMode>();
        foreach (var requestGameMode in request.GameModes)
        {
            if (!Enum.TryParse<GameMode>(requestGameMode, true, out var gameMode))
                throw new ArgumentException($"Invalid game mode '{requestGameMode}'");
            gameModes.Add(gameMode);
        }
        
        var publisher = new Publisher(request.Publisher);
        
        var developers = new List<Developer>();
        foreach (var requestDeveloper in request.Developers)
        {
            developers.Add(new Developer(requestDeveloper));
        }
        
        var platforms = new List<Platform>();
        foreach (var requestPlatform in request.Platforms)
        {
            if(!Enum.TryParse<Platform>(requestPlatform, true, out var platform))
                throw new ArgumentException($"Invalid platform '{requestPlatform}'");
            platforms.Add(platform);
        }
        
        var game = Game.CreateGame(
            request.Title,
            request.Description,
            request.Story,
            request.Franchise,
            request.ReleaseDate,
            ageRating,
            gameModes,
            publisher,
            developers,
            platforms);
        
        await _gameRepository.AddAsync(game);
        
        var response = new CreateGameResponse()
        {
            Id = game.Id.ToString(),
            Title = game.Title,
            Description = game.Description,
            Story = game.Story,
            Franchise = game.Franchise,
            ReleaseDate = game.ReleaseDate,
            AgeRating = game.AgeRating.ToString(),
            Developers = game.Developers.Select(developer => developer.Name).ToList(),
            Platforms = game.Platforms.Select(platform => platform.ToString()).ToList(),
            GameModes = game.GameModes.Select(gameMode => gameMode.ToString()).ToList(),
            Publisher =  publisher.ToString(),
        };
        return response;
    }
}