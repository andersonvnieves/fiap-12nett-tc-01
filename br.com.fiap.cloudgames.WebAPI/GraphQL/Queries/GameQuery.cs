using br.com.fiap.cloudgames.Application.UseCases.Game.RetrieveGame;

namespace br.com.fiap.cloudgames.WebAPI.GraphQL.Queries;

public class GameQuery
{
    public async Task<RetrieveGameResponse> Game([Service] RetrieveGameUseCase useCase, string id)
    {
        return await useCase.ExecuteAsync(new RetrieveGameRequest() { GameId = id });
    }
}