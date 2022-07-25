using System.Text;
using Microsoft.EntityFrameworkCore;
using MarvelAPI.Data;
using MarvelAPI.Services.MovieAppearance;
using MarvelAPI.Services.Character;
using MarvelAPI.Services.MoviesService;
using MarvelAPI.Services.TVShowsService;
using MarvelAPI.Services.TVShowAppearance;
using MarvelAPI.Services.User;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MarvelAPI.Services.Token;

var builder = WebApplication.CreateBuilder(args);

//Add connection string and DbContext setup

// * String builder used for user-secrets
// var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnectionUS"));
// conStrBuilder.Password = builder.Configuration["Password"];
// * END of String builder calls

// ? Connection string variables
// * String builder for user-secrets
// var connection = conStrBuilder.ConnectionString;

// * "DefaultConnectionTrust" for integrated security (Windows)
var connection = builder.Configuration.GetConnectionString("DefaultConnectionTrust");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

// Add Services/Interfaces for Dependency Injection here
builder.Services.AddScoped<IMovieAppearanceService, MovieAppearanceService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<ITVShowService, TVShowService>();
builder.Services.AddScoped<ITVShowAppearanceService, TVShowAppearanceService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication
    (
        JwtBearerDefaults.AuthenticationScheme
    )
    .AddJwtBearer(
        options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
                )
            };
        }
    );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adds AuthenticationMiddleware to IApplicationBuilder
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
