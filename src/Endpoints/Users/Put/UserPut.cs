using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UnsualLotery.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace UnsualLotery.Endpoints.Users.Put;

public class UserPut
{
    public static string Template => "/users/{UserId:Guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handler => Action;

    [Authorize]
    [EnableCors]
    public static async Task<IResult> Action(Guid UserId, UserRequest clientRequest, UserManager<IdentityUser> userManager)
    {
        var user = await userManager.FindByIdAsync(UserId.ToString());

        if (user == null)
        {
            return Results.NotFound();
        }

        var claims = userManager.GetClaimsAsync(user).Result;

        var userClaimns = new List<Claim>
        {
            new Claim("FirstName", clientRequest.FirstName),
            new Claim("LastName", clientRequest.LastName)
        };

        for (int i = 0; i < userClaimns.Count; i++)
        {
            await userManager.ReplaceClaimAsync(user, claims[i], userClaimns[i]);
        }

        return Results.Ok();
    }
}

