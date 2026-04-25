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

    public UserRepository(ILogger<UserRepository> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
        _users = _context.Set<User>();
    }
    
    public async Task AddAsync(User user)
    {
        await _users.AddAsync(user);
    }

    public User GetByIdentityId(string identityId)
    {
        return _users.FirstOrDefault(u => u.IdentityId == identityId);
    }
}