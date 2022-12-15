using System;
using System.Security.Claims;
using UnsualLotery.Domain.Lotery;
using UnsualLotery.Infra.Data;

namespace UnsualLotery.Endpoints.Lotery;

public class RafflePost {
    public static string Template => "/raffles";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action(
        RaffleRequest raffleRequest, HttpContext http, ApplicationDbContext context) {

        var raffle = new Raffle(raffleRequest.Value, raffleRequest.AvailableQuantity, raffleRequest.TotalQuantity, raffleRequest.Active);

        await context.Raffles.AddAsync(raffle);
        await context.SaveChangesAsync();

        return Results.Created($"/raffles/{raffle.Id}", raffle.Id);
    }
}

