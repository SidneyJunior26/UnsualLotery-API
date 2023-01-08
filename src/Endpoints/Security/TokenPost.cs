using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UnsualLotery.Services.Security;
using Swashbuckle.AspNetCore.Annotations;
using UnsualLotery.Services.Validations;

namespace UnsualLotery.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/users/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    /// <summary>
    /// Endpoint para criar token JWT
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <param name="config"></param>
    /// <param name="userManager"></param>
    /// <param name="environment"></param>
    /// <returns></returns>
    [SwaggerResponse(statusCode: 201, description: "Sucesso criar token do usuário", Type = typeof(string))]
    [SwaggerResponse(statusCode: 400, description: "Usuário não encontrado", Type = typeof(ValidateModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(GenericErrorViewModel))]
    [AllowAnonymous]
    public static async Task<IResult> Action(
        LoginRequest loginRequest, IConfiguration config,
        UserManager<IdentityUser> userManager,
        IWebHostEnvironment environment
        )
    {
        var user = await userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null)
            return Results.NotFound();

        if (!await userManager.CheckPasswordAsync(user, loginRequest.Password))
            return Results.NotFound();

        var claims = await userManager.GetClaimsAsync(user);
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim("Email", loginRequest.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("PhoneNumber", user.PhoneNumber)
        });

        subject.AddClaims(claims);

        var tokenDescription = new SecurityService(config).GetTokenDescriptor(subject);

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);

        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });
    }
}

