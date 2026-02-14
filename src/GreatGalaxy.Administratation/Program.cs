//using VehicleService.Application;
//using VehicleService.Infrastructure;

using GreatGalaxy.Administration.Repositories;
using GreatGalaxy.Administration.Requests.Driver;
using GreatGalaxy.Administration.Requests.Vehicle;
using GreatGalaxy.Administration.Services;
using GreatGalaxy.Common.ValueTypes;

var builder = WebApplication.CreateBuilder(args);

// Vehicle services
builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
builder.Services.AddSingleton<IVehicleService, VehicleService>();

// Driver services
builder.Services.AddSingleton<IDriverRepository, DriverRepository>();
builder.Services.AddSingleton<IDriverService, DriverService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


// Using this simple implementation to save time,
// but in a real application we would have more complex logic and error handling here,
// and we would also have endpoints for updating and deleting vehicles, as well as for managing other entities like drivers, routes, etc.

// Vehicles
app.MapPut("/vehicles", (CreateVehicleRequest request, IVehicleService service) =>
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

app.MapPost("/vehicles", (UpdateVehicleRequest request, IVehicleService service) =>
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
    var driver = service.Get(new DriverId(id));
    return Results.Ok(service.Retire(driver));
});

app.MapPost("/drivers/{id:int}/reactivate", (int id, IDriverService service) =>
{
    var driver = service.Get(new DriverId(id));
    return Results.Ok(service.Reactivate(driver));
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


app.Run();


