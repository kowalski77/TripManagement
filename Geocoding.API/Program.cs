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

app.MapGet("/locations/coordinates", (double latitude, double longitude, ILogger<Program> logger) =>
{
    logger.LogInformation("Requested received");

    Fixture fixture = new();
    fixture.Customize<AddressComponent>(c => c.With(x => x.City, "Sabadell"));
    fixture.Customize<Location>(c => c.With(x => x.Latitude, 41.32).With(x => x.Longitude, Random.Shared.Next(2, 5)));

    Place place = fixture.Create<Place>();

    return Results.Ok(place);
})
    .WithName("GetLocationByCoordinates")
    .WithOpenApi();

app.Run();
