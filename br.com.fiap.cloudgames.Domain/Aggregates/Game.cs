using br.com.fiap.cloudgames.Domain.Entities;
using br.com.fiap.cloudgames.Domain.Enums;

namespace br.com.fiap.cloudgames.Domain.Aggregates;

public class Game
{
    public Game() { } //ORM
    
    #region FactoryMethod
    public static Game CreateGame(
        String title, 
        String description, 
        String story, 
        String franchise, 
        DateOnly releaseDate, 
        AgeRating ageRating, 
        List<GameModes> gameModes,
        Publisher publisher,
        List<Developer> developers,
        List<Platforms> platforms)
    {
        var game = new Game()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Story = story,
            Franchise = franchise,
            ReleaseDate = releaseDate,
            AgeRating = ageRating,
            GameModes = gameModes,
            Publisher = publisher,
            Developers = developers,
            Platforms = platforms
        };
        Validate(title, description, story, releaseDate, ageRating, gameModes, developers, platforms);
        return game;
    }
    #endregion
    
    #region Properties
    public Guid Id { get; private set; }
    public String Title { get; private set; }
    public String Description { get; private set; }
    public String Story { get; private set; }
    public String Franchise { get; private set; }
    public DateOnly ReleaseDate { get; private set; }
    public AgeRating AgeRating { get; private set; }
    public List<GameModes> GameModes { get; private set; }
    public Publisher Publisher { get; private set; }
    public List<Developer> Developers { get; private set; }
    public List<Platforms> Platforms { get; private set; }
    #endregion

    private static void Validate(
        string title,
        string description,
        string story,
        DateOnly releaseDate,
        AgeRating ageRating,
        List<GameModes> gameModes,
        List<Developer> developers,
        List<Platforms> platforms)
    {
        if (String.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(Title));
        
        if (String.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(Description));
        
        if (String.IsNullOrWhiteSpace(story))
            throw new ArgumentNullException(nameof(Story));
        
        if (releaseDate > DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentOutOfRangeException(nameof(ReleaseDate));
        
        if (gameModes == null || !gameModes.Any())
            throw new ArgumentNullException(nameof(GameModes));
        
        if(developers == null || !developers.Any())
            throw new ArgumentNullException(nameof(Developers));
        
        if (platforms == null || !platforms.Any())
            throw new ArgumentNullException(nameof(Platforms));
    }
}