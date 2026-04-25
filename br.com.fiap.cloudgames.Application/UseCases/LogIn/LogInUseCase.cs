using br.com.fiap.cloudgames.Application.Services;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Domain.ValueObjects;

namespace br.com.fiap.cloudgames.Application.UseCases.LogIn;

public class LogInUseCase
{
    private readonly IUserAuthService _userAuthService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LogInUseCase(IUserAuthService userAuthService, IUserRepository userRepository, ITokenService tokenService)
    {
        _userAuthService = userAuthService;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LogInResponse> ExecuteAsync(LogInRequest request)
    {
        var email = new EmailAddress(request.email);
        var identityUserId = await _userAuthService.AuthenticateUserAsync(email.Email, request.password);
        var user = await _userRepository.GetByIdentityIdAsync(identityUserId);
        var token = await _tokenService.GenerateTokenAsync(user);

        return new LogInResponse()
        {
            Token =  token
        };
    }
}