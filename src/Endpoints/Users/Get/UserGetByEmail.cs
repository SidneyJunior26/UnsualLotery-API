using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UnsualLotery.Infra.Data;

namespace UnsualLotery.Endpoints.Users.Get;

public class UserGetByEmail {

    public static string Template => "/users/{Email}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static async Task<IResult> Action(string Email, UserManager<IdentityUser> userManager) {
        var user = userManager.Users
            .FirstOrDefault(u => u.Email == Email);

        if (user == null) {
            return Results.NotFound();
        }

        var claims = userManager.GetClaimsAsync(user).Result;

        var claimFirstName = claims.FirstOrDefault(c => c.Type == "FirstName");
        var claimLastName = claims.FirstOrDefault(c => c.Type == "LastName");

        var firstName = claimFirstName?.Value != null ? claimFirstName.Value : "";
        var lastName = claimLastName?.Value != null ? claimLastName.Value : "";

        var result = new UserResponse(firstName, lastName, user.Email, user.PhoneNumber);

        return Results.Ok(result);
    }
}

