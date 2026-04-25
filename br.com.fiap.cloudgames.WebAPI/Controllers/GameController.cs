using br.com.fiap.cloudgames.Application.UseCases.CreateGame;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace br.com.fiap.cloudgames.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        public readonly  CreateGameUseCase _createGameUseCase;
        public GameController(CreateGameUseCase createGameUseCase)
        {
            _createGameUseCase = createGameUseCase;
        }
        
        [Authorize(Roles =  "admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGameRequest request)
        {
            var result = await _createGameUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get(String Id)
        {
            return Ok();
        }
    }
}
