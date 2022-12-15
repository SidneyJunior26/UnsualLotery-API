using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using UnsualLotery.Services.Users;

namespace UnsualLotery.Endpoints.Users;

public class UserPost {

    public static string Template => "/users";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action(UserRequest clientRequest, UserCreatorService userCreator) {

        var userClaimns = new List<Claim>
        {
            new Claim("FirstName", clientRequest.FirstName),
            new Claim("LastName", clientRequest.LastName)
        };

        (IdentityResult result, string userId) result = await userCreator.Create(
            clientRequest.Email, clientRequest.Password, clientRequest.PhoneNumber, userClaimns);

        if (!result.result.Succeeded)
            return Results.BadRequest(result.result.Errors.First());

        return Results.Created($"/users/{result.userId}", result.userId);
    }
}

