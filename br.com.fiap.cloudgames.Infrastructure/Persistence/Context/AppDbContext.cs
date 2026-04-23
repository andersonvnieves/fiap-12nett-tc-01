using br.com.fiap.cloudgames.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace br.com.fiap.cloudgames.Infrastructure.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    public DbSet<Game> Games { get; set; }
}