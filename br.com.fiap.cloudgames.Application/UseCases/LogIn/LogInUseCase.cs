using br.com.fiap.cloudgames.Application.Services;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace br.com.fiap.cloudgames.Application.UseCases.LogIn;

public class LogInUseCase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private const string LOG_IN_ERROR_MESSAGE = "Email or Password invalid";

    public LogInUseCase(UserManager<IdentityUser> userManager, IUserRepository userRepository, ITokenService tokenService)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LogInResponse> ExecuteAsync(LogInRequest request)
    {
        var email = new EmailAddress(request.email);
        
        var identityUser = await _userManager.FindByEmailAsync(email.Email);
        if (identityUser == null)
            throw new Exception(LOG_IN_ERROR_MESSAGE);

        var isPasswordValid = await _userManager.CheckPasswordAsync(identityUser, request.password);
        if (!isPasswordValid)
            throw new Exception(LOG_IN_ERROR_MESSAGE);
        
        var user = _userRepository.GetByIdentityId(identityUser.Id);
        
        var token = await _tokenService.GenerateTokenAsync(identityUser, user);

        return new LogInResponse()
        {
            Token =  token
        };
    }
}