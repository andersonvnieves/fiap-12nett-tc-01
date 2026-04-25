using br.com.fiap.cloudgames.Application.UseCases.ChangeUserRole;
using br.com.fiap.cloudgames.Application.UseCases.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace br.com.fiap.cloudgames.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly ChangeUserRoleUseCase _changeUserRoleUseCase;
        private const string ADMIN_ROLE = "admin";
        public UserController(RegisterUserUseCase registerUserUseCase,  ChangeUserRoleUseCase changeUserRoleUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _changeUserRoleUseCase = changeUserRoleUseCase;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserRequest request)
        {
            var result = await _registerUserUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [Authorize(Roles = ADMIN_ROLE)]
        [HttpPut("role")]
        public async Task<IActionResult> Put([FromBody] ChangeUserRoleRequest request)
        {
            var result = await _changeUserRoleUseCase.ExecuteAsync(request);
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(String Id)
        {
            return Ok();
        }
    }
}
