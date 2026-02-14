namespace GreatGalaxy.Dispatch.Requests
{
    public record CreateDeliveryRequest(
    int DriverId,
    int VehicleId,
    int RouteId);
}
