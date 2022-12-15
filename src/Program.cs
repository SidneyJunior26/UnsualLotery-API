using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using UnsualLotery.Endpoints.Lotery;
using UnsualLotery.Endpoints.Lotery.Raffles;
using UnsualLotery.Endpoints.Security;
using UnsualLotery.Endpoints.Users;
using UnsualLotery.Endpoints.Users.Get;
using UnsualLotery.Endpoints.Users.Put;
using UnsualLotery.Infra.Data;
using UnsualLotery.Services.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<ApplicationDbContext>(
    builder.Configuration["ConnectionStrings:UnsualLotery"]);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
});


builder.Services.AddScoped<UserCreatorService>();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add Methods
app.MapMethods(RafflePost.Template, RafflePost.Methods, RafflePost.Handler);
app.MapMethods(RaffleGetAll.Template, RaffleGetAll.Methods, RaffleGetAll.Handler);
app.MapMethods(RaffleGetActives.Template, RaffleGetActives.Methods, RaffleGetActives.Handler);
app.MapMethods(RaffleGetById.Template, RaffleGetById.Methods, RaffleGetById.Handler);

app.MapMethods(UserPost.Template, UserPost.Methods, UserPost.Handler);
app.MapMethods(UserPut.Template, UserPut.Methods, UserPut.Handler);
app.MapMethods(UserGetById.Template, UserGetById.Methods, UserGetById.Handler);
app.MapMethods(UserGetByEmail.Template, UserGetByEmail.Methods, UserGetByEmail.Handler);

app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handler);

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext http) => {
    var error = http.Features?.Get<IExceptionHandlerFeature>()?.Error;

    if (error != null) {
        switch (error) {
            case SqlException:
                return Results.Problem(title: "Databse out", statusCode: 500);
            case FormatException:
                return Results.Problem(title: "Error to convert data to other type. Confirm all information sent", statusCode: 500);
        }
    }

    return Results.Problem(title: "An error ocurred", statusCode: 500);
});

app.Run();