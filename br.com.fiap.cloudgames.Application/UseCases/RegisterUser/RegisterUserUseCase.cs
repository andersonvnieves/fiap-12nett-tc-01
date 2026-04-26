using br.com.fiap.cloudgames.Application.Services;
using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Application.UnitsOfWork;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace br.com.fiap.cloudgames.Application.UseCases.RegisterUser;

public class RegisterUserUseCase
{
    private readonly IUserAuthService _userAuthService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository; 
    private readonly ILogger<RegisterUserUseCase> _logger;

    public RegisterUserUseCase(IUserAuthService userAuthService,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        ILogger<RegisterUserUseCase> logger)
    {
        _userAuthService = userAuthService;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request)
    {
        try
        {
            _logger.LogInformation("Executing {UseCase}. Email={Email}", nameof(RegisterUserUseCase), request.Email);

            var name = new Name(request.FirstName, request.LastName);
            var email = new EmailAddress(request.Email);
            var role = UserRoles.user.ToString();
            await _unitOfWork.BeginTransactionAsync();
            
            var identityUserId = await _userAuthService.CreateUserAsync(request.Email, request.Password, role);
            
            var user = User.Create(name, email, identityUserId);
            await _userRepository.AddAsync(user);
        
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("User registered successfully. UserId={UserId}, Email={Email}", user.Id, user.Email.Email);
            return new RegisterUserResponse()
            {
                Id = user.Id.ToString(),
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                Email = user.Email.Email,
                Role = user.Role.ToString()
            };
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _logger.LogError(ex, "Error executing {UseCase}. Email={Email}", nameof(RegisterUserUseCase), request.Email);
            throw;
        }
    }
    
    
}