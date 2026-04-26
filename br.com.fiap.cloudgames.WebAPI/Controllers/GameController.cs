using br.com.fiap.cloudgames.Application.UseCases.CreateGame;
using br.com.fiap.cloudgames.Application.UseCases.RetrieveGame;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace br.com.fiap.cloudgames.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly  CreateGameUseCase _createGameUseCase;
        private readonly  RetrieveGameUseCase _retrieveGameUseCase;
        private const string ADMIN_ROLE = "admin";
        public GameController(CreateGameUseCase createGameUseCase,  RetrieveGameUseCase retrieveGameUseCase)
        {
            _createGameUseCase = createGameUseCase;
            _retrieveGameUseCase = retrieveGameUseCase;
        }
        
        [Authorize(Roles = ADMIN_ROLE)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGameRequest request)
        {
            var result = await _createGameUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] String Id)
        {
            var result = await _retrieveGameUseCase.ExecuteAsync(new RetrieveGameRequest() { GameId = Id });
            return Ok(result);
        }
    }
}
