using System.Diagnostics;
using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace br.com.fiap.cloudgames.Infrastructure.Persistence.Repositories;

public class GameRepository : IGameRepository
{
    private readonly ILogger<GameRepository> _logger;
    private readonly AppDbContext _context;
    private readonly DbSet<Game> _gamesDbSet;

    public GameRepository(AppDbContext context)
    {
        _context = context;
        _gamesDbSet = context.Games;
    }
    
    public async Task AddAsync(Game game)
    {
        var stopwatch = Stopwatch.StartNew();
        await _gamesDbSet.AddAsync(game);
        await _context.SaveChangesAsync();
        stopwatch.Stop();
        _logger.LogInformation("Duration in ms: " + stopwatch.ElapsedMilliseconds);
    }
}