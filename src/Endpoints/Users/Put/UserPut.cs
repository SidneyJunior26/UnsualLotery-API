using System;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UnsualLotery.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Swashbuckle.AspNetCore.Annotations;
using UnsualLotery.Services.Validations;

namespace UnsualLotery.Endpoints.Users.Put;

public class UserPut
{
    public static string Template => "/users/{UserId:Guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handler => Action;

    /// <summary>
    /// Endpoint para atualizar dados do usuário
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="clientRequest"></param>
    /// <param name="userManager"></param>
    /// <returns></returns>
    [SwaggerResponse(statusCode: 201, description: "Sucesso atualizar usuário")]
    [SwaggerResponse(statusCode: 404, description: "Usuário não encontrado", Type = typeof(ValidateModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
    [Authorize]
    [EnableCors]
    public static async Task<IResult> Action(Guid UserId, UserRequest clientRequest, UserManager<IdentityUser> userManager)
    {
        var user = await userManager.FindByIdAsync(UserId.ToString());

        if (user == null)
            return Results.NotFound();

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

