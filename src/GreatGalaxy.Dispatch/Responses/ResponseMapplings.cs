using GreatGalaxy.Dispatch.Models;

namespace GreatGalaxy.Dispatch.Responses
{
    public static class DriverMappings
    {
        public static LocationResponse ToDto(this Location location)
        {
            return new LocationResponse(
                location.Id.HasValue ? location.Id.Value.Value : 0,
                location.Address.Line1,
                location.Address.Line2,
                location.Address.Line3,
                location.Address.GPSCoordinates.Latitude,
                location.Address.GPSCoordinates.Longitude,
                location.Address.CelestialBody.Name,
                location.Address.CelestialBody.Position.Position
            );
        }

        public static DeliveryRouteResponse ToDto(this DeliveryRoute route)
        {
            return new DeliveryRouteResponse(
                route.Id.HasValue ? route.Id.Value.Value : 0,
                route.Origen.ToDto(),
                route.Checkpoints.Select(c => c.ToDto()).ToList(),
                route.Destination.ToDto(),
                route.Distance
            );
        }

        public static DeliveryResponse ToDto(this Delivery delivery)
        {
            return new DeliveryResponse(
                delivery.Id.HasValue ? delivery.Id.Value.Value : 0,
                delivery.RouteId.Value,
                delivery.Departed,
                delivery.Arrived,
                delivery.DriverId.Value,
                delivery.VehicleId.Value,
                delivery.Events?.Select(e => e.ToDto()).ToList(),
                delivery.Status,
                delivery.CheckpointsReached.Select(c => new CheckpointReachedResponse(c.LocationId.Value, c.Timestamp)).ToList()
            );
        }

        public static DeliveryEventResponse ToDto(this DeliveryEvent deliveryEvent)
        {
            return new DeliveryEventResponse(
                deliveryEvent.EventId,
                deliveryEvent.EventType,
                deliveryEvent.Timestamp,
                deliveryEvent.Duration,
                deliveryEvent.RelatedCheckpoint.HasValue ? deliveryEvent.RelatedCheckpoint.Value.Value : null,
                deliveryEvent.Description
            );
        }
    }
}
