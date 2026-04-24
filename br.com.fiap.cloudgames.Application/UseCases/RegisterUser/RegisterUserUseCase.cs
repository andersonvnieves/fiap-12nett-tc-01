using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Domain.UnitsOfWork;
using br.com.fiap.cloudgames.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace br.com.fiap.cloudgames.Application.UseCases.RegisterUser;

public class RegisterUserUseCase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository; 

    public RegisterUserUseCase(UserManager<IdentityUser> userManager,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var identityUser = new IdentityUser()
            {
                UserName = request.Email,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, request.Password);
            if(!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ArgumentException($"Error creating user: {errors}");
            }

            var name = new Name(request.FirstName, request.LastName);
            var email = new EmailAddress(request.Email);
        
            var user = User.Create(name, email, identityUser.Id);
            await _userRepository.AddAsync(user);
        
            await _unitOfWork.CommitAsync();
            return new RegisterUserResponse()
            {
                Id = user.Id.ToString(),
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                Email = user.Email.Email
            };
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}