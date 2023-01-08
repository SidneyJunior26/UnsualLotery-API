using System;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using UnsualLotery.Domain.Lotery;
using UnsualLotery.Infra.Data;
using UnsualLotery.Services.Validations;

namespace UnsualLotery.Endpoints.Lotery.Raffles.Get;

public class RaffleGetAll {
    public static string Template => "/raffles";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    /// <summary>
    /// Endpoint para listar todos os sorteios (ativos e não ativos)
    /// </summary>
    /// <param name="context"></param>
    /// <returns>Retorna status ok contendo as informações dos sorteios (ativos e não ativos)</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao consultar sorteio", Type = typeof(IEnumerable<RaffleResponse>))]
    [SwaggerResponse(statusCode: 404, description: "Não foi enncontrado nenhum sorteio", Type = typeof(ValidateModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
    public static IResult Action(ApplicationDbContext context) {
        List<Raffle> raffle = context.Raffles.OrderByDescending(r => r.CreatedOn).ToList();

        if (raffle == null)
            return Results.NotFound();

        var response = raffle.Select(r => new RaffleResponse(r.Id, r.Value, r.AvailableQuantity, r.TotalQuantity, r.Active, r.CreatedOn));

        return Results.Ok(response);
    }
}

