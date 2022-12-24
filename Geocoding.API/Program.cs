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

app.MapGet("/locations/coordinates", (double latitude, double longitude) =>
{
    Fixture fixture = new();
    fixture.Customize<AddressComponent>(c => c.With(x => x.City, "Sabadell"));
    fixture.Customize<Location>(c => c.With(x => x.Latitude, 41.32).With(x => x.Longitude, 2.06));

    Place place = fixture.Create<Place>();

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
