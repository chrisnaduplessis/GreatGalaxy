using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Reporting.Repositories;
using GreatGalaxy.Reporting.Services;

namespace GreatGalaxy.Reporting
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register services
            builder.Services.AddSingleton<IDriverRepository, DriverRepository>();
            builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
            builder.Services.AddSingleton<IRouteRepository, RouteRepository>();
            builder.Services.AddSingleton<IDeliveryRepository, DeliveryRepository>();

            builder.Services.AddSingleton<ITripReportCreator, TripReportCreator>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.MapGet("/trips/{deliveryId:int}",
                (int deliveryId, ITripReportCreator service) =>
                {
                    var route = service.CreteReport(new DeliveryId(deliveryId));

                    return route is null
                        ? Results.NotFound()
                        : Results.Ok(route);
                });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}