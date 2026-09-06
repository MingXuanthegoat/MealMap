using Microsoft.EntityFrameworkCore;
using MealMap.Api.Data;
using Scalar.AspNetCore;



var builder = WebApplication.CreateBuilder(args);

// Register the DbContext
builder.Services.AddDbContext<MealMapDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers(); // For controller support
var app = builder.Build();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

}

app.UseHttpsRedirection();
app.Run();
