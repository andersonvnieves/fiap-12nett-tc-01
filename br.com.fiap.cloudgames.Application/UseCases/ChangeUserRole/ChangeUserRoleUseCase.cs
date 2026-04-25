using br.com.fiap.cloudgames.Application.Services;
using br.com.fiap.cloudgames.Application.UnitsOfWork;
using br.com.fiap.cloudgames.Domain.Enums;
using br.com.fiap.cloudgames.Domain.Repositories;

namespace br.com.fiap.cloudgames.Application.UseCases.ChangeUserRole;

public class ChangeUserRoleUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAuthService _userAuthService;
    private readonly IUnitOfWork _unitOfWork;
    
    public ChangeUserRoleUseCase(IUserRepository userRepository, IUserAuthService userAuthService, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userAuthService = userAuthService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ChangeUserRoleResponse> ExecuteAsync(ChangeUserRoleRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            if (user == null)
                throw new ArgumentException("User not found.");

            var role = Enum.Parse<UserRoles>(request.Role.ToLower());
            await _userAuthService.ReplaceUserRoleAsync(user.IdentityId, role.ToString().ToLower());

            user.Role = Enum.Parse<UserRoles>(request.Role);
            _userRepository.Update(user);

            await _unitOfWork.CommitAsync();

            return new ChangeUserRoleResponse()
            {
                Id =  user.Id.ToString(),
                FirstName = user.Name.FirstName,
                LastName = user.Name.LastName,
                Email = user.Email.Email,
                Role = role.ToString()
            };
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}