using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Reporting.DomainModel;
using GreatGalaxy.Reporting.Models;
using GreatGalaxy.Reporting.Repositories;
using GreatGalaxy.Repository.Entities.Location;

namespace GreatGalaxy.Reporting.Services
{
    public class TripReportCreator : ITripReportCreator
    {
        private readonly IDeliveryRepository deliveryRepository;
        private readonly IDriverRepository driverRepository;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IRouteRepository routeRepository;

        public TripReportCreator(
            IDeliveryRepository deliveryRepository,
            IDriverRepository driverRepository,
            IVehicleRepository vehicleRepository,
            IRouteRepository routeRepository)
        {
            this.deliveryRepository = deliveryRepository;
            this.driverRepository = driverRepository;
            this.vehicleRepository = vehicleRepository;
            this.routeRepository = routeRepository;
        }

        /// <inheritdoc/>
        public TripReport CreteReport(DeliveryId deliveryId)
        {
            var delivery = deliveryRepository.Get(deliveryId);

            if (delivery == null)
            {
                throw new ArgumentException($"Delivery with id {deliveryId} not found.");
            }

            var driverEntity = this.driverRepository.Get(delivery.DriverId);
            var driver = new Driver(
                driverEntity.Id,
                driverEntity.Name);

            var vehicleEntity = this.vehicleRepository.Get(delivery.VehicleId);
            var vehicle = new Vehicle(
                vehicleEntity.Id,
                new Common.ValueTypes.Vehicle.VehicleType(vehicleEntity.Make, vehicleEntity.Model),
                vehicleEntity.Description);

            var routeEntity = this.routeRepository.Get(delivery.RouteId);

            var events = delivery.Events.Select(e => new DeliveryEvent(e.EventId, e.EventType.ToString(), e.Timestamp, e.Duration, e.RelatedCheckpoint, e.Description)).ToList();

            var checkpoints = routeEntity.Checkpoints.Select(c =>
            new Checkpoint(
                MapToLocation(c),
                delivery.CheckpointReached?.Find(_ => _.LocationId == c.Id)?.Timestamp,
                events.Where(_ => _.RelatedCheckpoint.HasValue && _.RelatedCheckpoint == c.Id).ToList())).ToList();

            var route = new DeliveryRoute(
                routeEntity.Id,
                MapToLocation(routeEntity.Origen),
                checkpoints,
                MapToLocation(routeEntity.Destination));
 
            return new TripReport
            {
                Id = delivery.Id,
                Route = route,
                Departed = delivery.Departed,
                Arrived = delivery.Arrived,
                Driver = driver,
                Vehicle = vehicle,
                Events = events,
                Status = delivery.Status
            };
        }

        private static Location MapToLocation(LocationEntity locationEntity)
        {
            return new Location(
                locationEntity.Id,
                locationEntity.Line1,
                locationEntity.Line2,
                locationEntity.Line3,
                locationEntity.CelestialBody,
                locationEntity.Location.Latitude,
                locationEntity.Location.Longitude,
                locationEntity.CelestialBodyPositionInSpace.Position);
        }
    }
}
