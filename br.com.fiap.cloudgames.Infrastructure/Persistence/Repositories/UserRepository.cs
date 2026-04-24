using System.Diagnostics;
using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace br.com.fiap.cloudgames.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly AppDbContext _context;
    private readonly DbSet<User>  _users;
    
    public async Task AddAsync(User user)
    {
        var stopwatch = Stopwatch.StartNew();
        await _users.AddAsync(user);
        await _context.SaveChangesAsync();
        stopwatch.Stop();
        _logger.LogInformation("Duration in ms: " + stopwatch.ElapsedMilliseconds);
    }
}