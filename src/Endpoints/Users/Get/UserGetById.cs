using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using UnsualLotery.Infra.Data;

namespace UnsualLotery.Endpoints.Users.Get;

public class UserGetById {

    public static string Template => "/users/{UserId:Guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    [Authorize]
    [EnableCors]
    public static async Task<IResult> Action(Guid UserId, UserManager<IdentityUser> userManager) {
        var user = await userManager.FindByIdAsync(UserId.ToString());

        if (user == null) {
            return Results.NotFound();
        }

        var claims = userManager.GetClaimsAsync(user).Result;

        var claimFirstName = claims.FirstOrDefault(c => c.Type == "FirstName");
        var claimLastName = claims.FirstOrDefault(c => c.Type == "LastName");

        var firstName = string.Empty;
        var lastName = string.Empty;

        if (claimFirstName != null) {
            firstName = claimFirstName.Value;
        }
        if (claimLastName != null) {
            lastName = claimLastName.Value;
        }

        var result = new UserResponse(firstName, lastName, user.Email, user.PhoneNumber);

        return Results.Ok(result);
    }
}

