using br.com.fiap.cloudgames.Domain.Aggregates;
using br.com.fiap.cloudgames.Domain.Repositories;
using br.com.fiap.cloudgames.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace br.com.fiap.cloudgames.Application.UseCases.RegisterUser;

public class RegisterUserUseCase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserRepository _userRepository;

    public RegisterUserUseCase(UserManager<IdentityUser> userManager,
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request)
    {
        var identityUser = new IdentityUser()
        {
            UserName = request.Email,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(identityUser, request.Password);
        if(!result.Succeeded)
            throw new Exception("An error occured while creating the user");

        var name = new Name(request.FirstName, request.LastName);
        var email = new EmailAddress(request.Email);
        
        var user = User.Create(name, email, identityUser.Id);
        await _userRepository.AddAsync(user);
        
        //TODO: Try to use a unit of work to create a transaction on this point.
        return new RegisterUserResponse()
        {
            Id = user.Id.ToString()
        };
    }
}