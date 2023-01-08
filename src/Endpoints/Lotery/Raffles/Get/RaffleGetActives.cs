using Swashbuckle.AspNetCore.Annotations;
using UnsualLotery.Domain.Lotery;
using UnsualLotery.Infra.Data;
using UnsualLotery.Services.Validations;

namespace UnsualLotery.Endpoints.Lotery.Raffles.Get;

public class RaffleGetActives {
    public static string Template => "/raffles/actives";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    /// <summary>
    /// Endpoint para listar todos os sorteios ativos
    /// </summary>
    /// <param name="context"></param>
    /// <returns>Retorna status ok contendo as informações dos sorteios ativos</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao consultar sorteio ativo", Type = typeof(IEnumerable<RaffleResponse>))]
    [SwaggerResponse(statusCode: 404, description: "Não foi encontrado nenhum sorteio ativo", Type = typeof(ValidateModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
    public static IResult Action(ApplicationDbContext context) {
        List<Raffle> raffles = context.Raffles.Where(r => r.Active == true).OrderByDescending(r => r.CreatedOn).ToList();

        if (raffles == null)
            return Results.NotFound();

        var response = raffles.Select(r => new RaffleResponse(r.Id, r.Value, r.AvailableQuantity, r.TotalQuantity, r.Active, r.CreatedOn));

        return Results.Ok(response);
    }
}

