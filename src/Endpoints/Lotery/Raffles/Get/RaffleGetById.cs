using System;
using UnsualLotery.Infra.Data;

namespace UnsualLotery.Endpoints.Lotery.Raffles;

public class RaffleGetById {

    public static string Template => "/raffles/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(Guid id, ApplicationDbContext context) {
        var raffles = context.Raffles
            .Where(r => r.Id == id)
            .OrderBy(r => r.CreatedBy)
            .ToList();

        if (raffles == null) {
            return Results.NotFound();
        }

        var response = raffles.Select(r => new RaffleResponse(r.Id, r.Value, r.AvailableQuantity, r.TotalQuantity, r.Active, r.CreatedOn));

        return Results.Ok(response);
    }
}

