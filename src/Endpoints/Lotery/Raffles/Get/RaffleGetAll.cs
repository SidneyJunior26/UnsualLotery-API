using System;
using Microsoft.AspNetCore.Authorization;
using UnsualLotery.Domain.Lotery;
using UnsualLotery.Infra.Data;

namespace UnsualLotery.Endpoints.Lotery.Raffles;

public class RaffleGetAll {
    public static string Template => "/raffles";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(ApplicationDbContext context) {
        List<Raffle> raffle = context.Raffles.ToList();

        if (raffle == null)
            return Results.NotFound();

        var response = raffle.Select(r => new RaffleResponse(r.Id, r.Value, r.AvailableQuantity, r.TotalQuantity, r.Active, r.CreatedOn));

        return Results.Ok(response);
    }
}

