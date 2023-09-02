using System.Data;
using Employee.API.Application.Inputs;
using Employee.API.Application.Services;
using Employee.API.Application.Validators;
using Employee.API.Infrastructure.Middlewares;
using Employee.API.Infrastructure.Repositories;
using FluentValidation;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var databaseConnection = builder.Configuration.GetSection("DATABASE_CONNECTION").Value;

builder.Services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(databaseConnection));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IValidator<CreateEmployeeInput>, CreateEmployeeInputValidator>();
builder.Services.AddScoped<IValidator<UpdateEmployeeInput>, UpdateEmployeeInputValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
