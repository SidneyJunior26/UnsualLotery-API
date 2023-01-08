using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using UnsualLotery.Domain.Lotery;
using UnsualLotery.Endpoints.Lotery.Raffles;
using UnsualLotery.Infra.Data;
using UnsualLotery.Services.Validations;

namespace UnsualLotery.Endpoints.Lotery;

public class RafflePost {
    public static string Template => "/raffles";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    /// <summary>
    /// Endpoint para cadastrar sorteio
    /// </summary>
    /// <param name="context"></param>
    /// <returns>Retorna status ok contendo as informações do novo sorteio cadastrado</returns>
    [SwaggerResponse(statusCode: 201, description: "Sucesso ao cadastrar sorteio", Type = typeof(Guid))]
    [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios não informados", Type = typeof(Dictionary<string, string[]>))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
    public static async Task<IResult> Action(
        RaffleRequest raffleRequest, HttpContext http, ApplicationDbContext context) {

        var raffle = new Raffle(Convert.ToDecimal(raffleRequest.Value), raffleRequest.AvailableQuantity, raffleRequest.TotalQuantity, raffleRequest.Active);

        if (!raffle.IsValid)
            return Results.ValidationProblem(raffle.Notifications.ConvertToProblemDetails());

        await context.Raffles.AddAsync(raffle);
        await context.SaveChangesAsync();

        return Results.Created($"/raffles/{raffle.Id}", raffle.Id);
    }
}

