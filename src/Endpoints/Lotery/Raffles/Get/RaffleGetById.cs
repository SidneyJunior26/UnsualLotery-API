using System;
using Swashbuckle.AspNetCore.Annotations;
using UnsualLotery.Infra.Data;
using UnsualLotery.Services.Validations;

namespace UnsualLotery.Endpoints.Lotery.Raffles;

public class RaffleGetById {

    public static string Template => "/raffles/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    /// <summary>
    /// Endpoint para consultar sorteio pelo ID
    /// </summary>
    /// <param name="context"></param>
    /// <returns>Retorna status ok contendo as informações do sorteio</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao consultar sorteio", Type = typeof(IEnumerable<RaffleResponse>))]
    [SwaggerResponse(statusCode: 404, description: "Não foi encontrado nenhum sorteio com este ID", Type = typeof(ValidateModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
    public static IResult Action(Guid id, ApplicationDbContext context) {
        var raffles = context.Raffles
            .Where(r => r.Id == id)
            .ToList();

        if (raffles == null)
            return Results.NotFound();

        var response = raffles.Select(r => new RaffleResponse(r.Id, r.Value, r.AvailableQuantity, r.TotalQuantity, r.Active, r.CreatedOn));

        return Results.Ok(response);
    }
}

