using System.Data;
using System.Text;
using Auth.API.Application.Inputs;
using Auth.API.Application.Services;
using Auth.API.Application.Validators;
using Auth.API.Infrastructure.Configurations;
using Auth.API.Infrastructure.Repositories;
using Auth.API.Infrastructure.Securities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var databaseConnection = configuration.GetSection("DATABASE_CONNECTION").Value;
var tokenConfiguration = configuration.GetSection("TokenConfiguration").Get<TokenConfiguration>()!;

builder.Services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(databaseConnection));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IValidator<CreateUserInput>, CreateUserInputValidator>();
builder.Services.AddScoped<IValidator<LoginInput>, LoginInputValidator>();

builder.Services.AddSingleton(tokenConfiguration);

builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = tokenConfiguration?.Issuer,
                    ValidAudience = tokenConfiguration?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MY API");
            });
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
