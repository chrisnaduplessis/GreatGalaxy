using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Dispatch.MessageConsumers;
using GreatGalaxy.Dispatch.Messages;
using GreatGalaxy.Dispatch.Repositories;
using GreatGalaxy.Dispatch.Requests;
using GreatGalaxy.Dispatch.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Route Services
builder.Services.AddSingleton<IRouteRepository, RouteRepository>();
builder.Services.AddSingleton<IRouteService, RouteService>();

// Delivery Services
builder.Services.AddSingleton<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddSingleton<IDeliveryService, DeliveryService>();

// MassTransit configuration
builder.Services.AddMassTransit(_ =>
{
    _.AddConsumer<DeliveryEventConsumer>();

    _.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Routes
app.MapPost("/routes", (CreateRouteRequest request, IRouteService service) =>
{
    var route = service.CreateRoute(
        new Address(request.OriginLine1, request.OriginLine2, request.OriginLine3, 
        new GPSCoordinates(request.OriginLatitude, request.OriginLongitude), new CelestialBody(request.OriginCelestialBody, new SpacePosition(request.OriginCelestialBodyPosition))),
        request.Checkpoints.Select(c => new Address(c.Line1, c.Line2, c.Line3, new GPSCoordinates(c.Latitude, c.Longitude), new CelestialBody(c.CelestialBody, new SpacePosition(c.CelestialBodyPosition)))).ToList(),
        new Address(request.DestinationLine1, request.DestinationLine2, request.DestinationLine3,
 new GPSCoordinates(request.DestinationLatitude, request.DestinationLongitude), new CelestialBody(request.DestinationCelestialBody, new SpacePosition(request.DestinationCelestialBodyPosition))));

    return Results.Created(
        $"/routes/{route.Id.Value}",
        route);
});

app.MapPost("/routes/{routeId:int}/checkpoints",
    (int routeId, AddCheckpointRequest request, IRouteService service) =>
    {
        var success = service.AddCheckpoint(
            new RouteId(routeId),
            new Address(request.Line1, request.Line2, request.Line3, new GPSCoordinates(request.Latitude, request.Longitude), new CelestialBody(request.CelestialBody, new SpacePosition(request.CelestialBodyPosition))));

        return success ? Results.NoContent() : Results.NotFound();
    });

app.MapGet("/routes/{routeId:int}",
    (int routeId, IRouteService service) =>
    {
        var route = service.GetRoute(new RouteId(routeId));

        return route is null
            ? Results.NotFound()
            : Results.Ok(route);
    });

app.MapGet("/routes",
    (IRouteService service) =>
    {
        return Results.Ok(service.GetAllRoutes());
    });

// Delivies
app.MapPost("/deliveries",
    (CreateDeliveryRequest request, IDeliveryService service) =>
    {
        var delivery = service.CreateDelivery(
            new DriverId(request.DriverId),
            new VehicleId(request.VehicleId),
            new RouteId(request.RouteId));

        return Results.Created(
            $"/deliveries/{delivery.Id.Value}",
            delivery);
    });

app.MapPost("/deliveries/{deliveryId:int}/cancel",
    (int deliveryId, IDeliveryService service) =>
    {
        var success = service.CancelDelivery(new DeliveryId(deliveryId));
        return success ? Results.NoContent() : Results.NotFound();
    });

app.MapPost("/deliveries/{deliveryId:int}/events",
    async (int deliveryId, AddDeliveryEventRequest request, IDeliveryService service) =>
    {
        var deliveryEvent = new DeliveryEventMessage(
            Guid.NewGuid(),
            new DeliveryId(request.DeliveryId),
            request.eventType,
            request.Timestamp,
            request.Duration,
            request.LocationId.HasValue ? new LocationId(request.LocationId.Value) : null,
            request.Description);

        // Post event here
        using var scope = app.Services.CreateScope();
        var publishEndpoint = scope.ServiceProvider
        .GetRequiredService<IPublishEndpoint>();

        await publishEndpoint.Publish(deliveryEvent);

        return Results.NoContent();
    });

app.MapGet("/deliveries/{deliveryId:int}",
    (int deliveryId, IDeliveryService service) =>
    {
        var delivery = service.GetDelivery(new DeliveryId(deliveryId));

        return delivery is null
            ? Results.NotFound()
            : Results.Ok(delivery);
    });

app.MapGet("/deliveries",
    (IDeliveryService service) =>
    {
        return Results.Ok(
            service.GetAllDeliveries());
    });


app.Run();