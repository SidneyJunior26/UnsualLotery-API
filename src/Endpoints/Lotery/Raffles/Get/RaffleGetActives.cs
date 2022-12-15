using System;
using UnsualLotery.Domain.Lotery;
using UnsualLotery.Infra.Data;

namespace UnsualLotery.Endpoints.Lotery.Raffles;

public class RaffleGetActives {
    public static string Template => "/raffles/actives";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(ApplicationDbContext context) {
        List<Raffle> raffles = context.Raffles.Where(r => r.Active == true).OrderBy(r => r.Value).ToList();

        if (raffles == null) {
            return Results.NotFound();
        }

        var response = raffles.Select(r => new RaffleResponse(r.Id, r.Value, r.AvailableQuantity, r.TotalQuantity, r.Active, r.CreatedOn));

        return Results.Ok(response);
    }
}

