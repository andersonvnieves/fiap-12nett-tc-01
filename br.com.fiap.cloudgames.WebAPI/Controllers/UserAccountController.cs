using br.com.fiap.cloudgames.Application.UseCases.RegisterUser;
using Microsoft.AspNetCore.Mvc;

namespace br.com.fiap.cloudgames.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;

        public UserAccountController(RegisterUserUseCase registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserRequest request)
        {
            var result = await _registerUserUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(String Id)
        {
            return Ok();
        }
    }
}
