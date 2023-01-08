using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Swashbuckle.AspNetCore.Annotations;
using UnsualLotery.Services.Users;
using UnsualLotery.Services.Validations;

namespace UnsualLotery.Endpoints.Users;

public class UserPost {

    public static string Template => "/users";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    /// <summary>
    /// Endpoint para cadastrar usuário
    /// </summary>
    /// <param name="clientRequest"></param>
    /// <param name="userCreator"></param>
    /// <returns></returns>
    [SwaggerResponse(statusCode: 201, description: "Sucesso cadastrar usuário", Type = typeof(Guid))]
    [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios não preenchidos", Type = typeof(ValidateModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
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

