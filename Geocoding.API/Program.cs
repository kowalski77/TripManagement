using AutoFixture;
using Geocoding.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/locations/coordinates", (decimal latitude, decimal longitude) =>
{
    Place place = new Fixture().Create<Place>();

    return Results.Ok(place);
})
    .WithName("GetLocationByCoordinates")
    .WithOpenApi();

app.MapGet("/locations/{id}", (int id) =>
{
    return Results.Ok();
})
    .WithName("GetLocationById")
    .WithOpenApi();

app.Run();
