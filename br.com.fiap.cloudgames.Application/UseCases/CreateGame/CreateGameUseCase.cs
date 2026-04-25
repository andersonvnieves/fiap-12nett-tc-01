using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Entities;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Application.UnitsOfWork;

namespace br.com.fiap.cloudgames.Application.UseCases.CreateGame;

public class CreateGameUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGameRepository  _gameRepository;
    
    public CreateGameUseCase(IUnitOfWork unitOfWork, IGameRepository gameRepository)
    {
        _unitOfWork = unitOfWork;
        _gameRepository = gameRepository; 
    }

    public async Task<CreateGameResponse> ExecuteAsync(CreateGameRequest request)
    {
        if (!Enum.TryParse<AgeRating>(request.AgeRating, true, out var ageRating))
            throw new ArgumentException($"Invalid age rating '{request.AgeRating}'");
        
        var gameModes = new List<GameModes>();
        foreach (var requestGameMode in request.GameModes)
        {
            if (!Enum.TryParse<GameModes>(requestGameMode, true, out var gameMode))
                throw new ArgumentException($"Invalid game mode '{requestGameMode}'");
            gameModes.Add(gameMode);
        }
        
        var publisher = new Publisher(request.Publisher);
        
        var developers = new List<Developer>();
        foreach (var requestDeveloper in request.Developers)
        {
            developers.Add(new Developer(requestDeveloper));
        }
        
        var platforms = new List<Platforms>();
        foreach (var requestPlatform in request.Platforms)
        {
            if(!Enum.TryParse<Platforms>(requestPlatform, true, out var platform))
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
        await _unitOfWork.CommitAsync();
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