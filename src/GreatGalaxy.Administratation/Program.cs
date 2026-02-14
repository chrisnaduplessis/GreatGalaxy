using GreatGalaxy.Administration.Repositories;
using GreatGalaxy.Administration.Requests.Driver;
using GreatGalaxy.Administration.Requests.EventType;
using GreatGalaxy.Administration.Requests.Planet;
using GreatGalaxy.Administration.Requests.Vehicle;
using GreatGalaxy.Administration.Services;
using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Vehicle;

var builder = WebApplication.CreateBuilder(args);

// Vehicle services
builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
builder.Services.AddSingleton<IVehicleService, VehicleService>();

// Driver services
builder.Services.AddSingleton<IDriverRepository, DriverRepository>();
builder.Services.AddSingleton<IDriverService, DriverService>();

// Event type services
builder.Services.AddSingleton<IEventTypeRepository, EventTypeRepository>();
builder.Services.AddSingleton<IEventTypeService, EventTypeService>();

// Planet services
builder.Services.AddSingleton<IPlanetRepository, PlanetRepository>();
builder.Services.AddSingleton<IPlanetService, PlanetService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


// Using this simple implementation to save time,
// but in a real application we would have more complex logic and error handling here

// Vehicles
app.MapPost("/vehicles", (CreateVehicleRequest request, IVehicleService service) =>
{
    var vehicle = service.Create(
        request.Make,
        request.Model,
        request.Description,
        request.WeightAllowanceKg,
        request.VolumeAllowanceM3,
        request.SpeedMetersPerSecond);

    return Results.Created($"/vehicles/{vehicle.Id.Value}", vehicle);
});

// Using patch here to allow partial update
app.MapPatch("/vehicles", (UpdateVehicleRequest request, IVehicleService service) =>
{
    var vehicle = service.Update(
        request.vehicleId,
        request.Description);

    return Results.Created($"/vehicles/{vehicle.Id.Value}", vehicle);
});

app.MapGet("/vehicles/{id:int}", (int id, IVehicleService service) =>
{
    var vehicle = service.Get(new VehicleId(id));
    return vehicle is null ? Results.NotFound() : Results.Ok(vehicle);
});

app.MapDelete("/vehicles/{id:int}", (int id, IVehicleService service) =>
{
    service.Scrap(new VehicleId(id));
    return Results.Ok();
});

app.MapGet("/vehicles/", (IVehicleService service) =>
{
    var vehicles = service.GetAll();
    return Results.Ok(vehicles);
});

// Driver
app.MapPost("/drivers", (CreateDriverRequest request, IDriverService service) =>
{
    var driver = service.Create(request.Name);
    return Results.Created($"/drivers/{driver.Id.Value}", driver);
});

app.MapPatch("/drivers/{id:int}", (int id, RenameDriverRequest request, IDriverService service) =>
{
    var driver = service.Rename(new DriverId(id), request.Name);
    return Results.Ok(driver);
});

app.MapPost("/drivers/{id:int}/retire", (int id, IDriverService service) =>
{
    return Results.Ok(service.Retire(new DriverId(id)));
});

app.MapPost("/drivers/{id:int}/reactivate", (int id, IDriverService service) =>
{
    return Results.Ok(service.Reactivate(new DriverId(id)));
});

app.MapPost("/drivers/{id:int}/approved-vehicles",
    (int id, ApproveVehicleRequest request, IDriverService service) =>
    {
        var driver = service.ApproveVehicle(
            new DriverId(id),
            new VehicleId(request.VehicleId));

        return Results.Ok(driver);
    });

app.MapDelete("/drivers/{id:int}/approved-vehicles/{vehicleId:int}",
    (int id, int vehicleId, IDriverService service) =>
    {
        var driver = service.RevokeVehicleApproval(
            new DriverId(id),
            new VehicleId(vehicleId));

        return Results.Ok(driver);
    });

app.MapGet("/drivers/{id:int}", (int id, IDriverService service) =>
{
    var driver = service.Get(new DriverId(id));
    return driver is null ? Results.NotFound() : Results.Ok(driver);
});

app.MapGet("/drivers", (bool? activeOnly, IDriverService service) =>
{
    var drivers = activeOnly == true
        ? service.GetAllActive()
        : service.GetAll();

    return Results.Ok(drivers);
});

// Event type
app.MapPost("/event-types",
    (CreateEventTypeRequest request, IEventTypeService service) =>
    {
        var eventType = service.Create(
            request.Name,
            request.Description,
            request.Category);

        return Results.Created(
            $"/event-types/{eventType.Id.Value}", eventType);
    });

app.MapPatch("/event-types/{id:int}",
    (int id, UpdateDescriptionRequest request, IEventTypeService service) =>
    {
        var success = service.UpdateDescription(
            new EventTypeId(id),
            request.Description);

        return success ? Results.NoContent() : Results.NotFound();
    });

app.MapGet("/event-types",
    (EventCategory? category, IEventTypeService service) =>
    {
        var result = category.HasValue
            ? service.GetByCategory(category.Value)
            : service.GetAll();

        return Results.Ok(result);
    });

// Planet
app.MapPost("/planets", (CreatePlanetRequest request, IPlanetService service) =>
{
    var planet = service.Create(request.Name);
    return Results.Created($"/planets/{planet.Id.Value}", planet);
});

app.MapPost("/planets/{planetId:int}/countries", (int planetId, CreateCountryRequest request, IPlanetService service) =>
{
    var planet = service.AddCountry(new PlanetId(planetId), request.Name);
    return Results.Ok(planet.Countries);
});

app.MapPost("/planets/{planetId:int}/countries/{countryId:int}/locations",
    (int planetId, int countryId, AddLocationRequest request, IPlanetService service) =>
    {
        var planet = service.AddLocation(
            new PlanetId(planetId),
            new CountryId(countryId),
            new Address(request.AddressLine1, request.AddressLine2, request.City, request.PostalCode));

        return Results.Ok(planet.Countries.First(_ => _.Id.Value.Value == countryId).Locations);
    });

app.MapGet("/planets/{planetId:int}", (int planetId, IPlanetService service) =>
{
    var planet = service.Get(new PlanetId(planetId));
    return planet is null ? Results.NotFound() : Results.Ok(planet);
});

app.MapGet("/planets", (string? name, IPlanetService service) =>
{
    var planets = name != null
        ? new[] { service.GetByName(name) }
        : service.GetAll();

    return Results.Ok(planets);
});
app.Run();


