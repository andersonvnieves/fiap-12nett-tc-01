using br.com.fiap.cloudgames.Domain.Repositories;

namespace br.com.fiap.cloudgames.Application.UseCases.RetrieveUser;

public class RetrieveUserUseCase
{
    private readonly IUserRepository _userRepository;

    public RetrieveUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RetrieveUserResponse> ExecuteAsync(RetrieveUserRequest request)
    {
        var parseResult = Guid.TryParse(request.UserId, out var UserId);
        if (!parseResult)
            throw new ArgumentException("Invalid UserId Format");

        var user = await _userRepository.GetUserByIdAsync(UserId);
        if(user == null)
            throw new Exception("User not found");

        return new RetrieveUserResponse()
        {
            Id = user.Id.ToString(),
            FirstName = user.Name.FirstName,
            LastName = user.Name.LastName,
            Email = user.Email.Email,
            UserAccountStatus = user.UserAccountStatus,
            Role = user.Role.ToString(),
            CreationDate = user.CreationDate
        };
    }
}