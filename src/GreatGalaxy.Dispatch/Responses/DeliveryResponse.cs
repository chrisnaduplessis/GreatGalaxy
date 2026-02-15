using GreatGalaxy.Common.ValueTypes.Delivery;

namespace GreatGalaxy.Dispatch.Responses
{
    public record DeliveryResponse(
        int Id,
        int RouteId,
        DateTime? Departed,
        DateTime? Arrived,
        int DriverId,
        int VehicleId,
        List<DeliveryEventResponse> Events,
        DeliveryStatus Status,
        List<CheckpointReachedResponse> CheckpointsReached);
}
