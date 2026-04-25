using br.com.fiap.cloudgames.Domain.Aggregates;
using Microsoft.AspNetCore.Identity;

namespace br.com.fiap.cloudgames.Application.Services;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(IdentityUser identityUser, User user); 
}