using br.com.fiap.cloudgames.Application.Services;
using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Application.UnitsOfWork;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Domain.ValueObjects;

namespace br.com.fiap.cloudgames.Application.UseCases.RegisterUser;

public class RegisterUserUseCase
{
    private readonly IUserAuthService _userAuthService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository; 

    public RegisterUserUseCase(IUserAuthService userAuthService,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _userAuthService = userAuthService;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request)
    {
        try
        {
            var name = new Name(request.FirstName, request.LastName);
            var email = new EmailAddress(request.Email);
            var role = UserRoles.user.ToString();
            await _unitOfWork.BeginTransactionAsync();
            
            var identityUserId = await _userAuthService.CreateUserAsync(request.Email, request.Password, role);
            
            var user = User.Create(name, email, identityUserId);
            await _userRepository.AddAsync(user);
        
            await _unitOfWork.CommitAsync();
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
            throw;
        }
    }
    
    
}