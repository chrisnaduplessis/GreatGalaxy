namespace GreatGalaxy.Dispatch.Messages
{
    public record DispatchEvent(Guid eventId, string Name, string Description, int EventTypeId, int VehicleId, int DriverId, DateTime Timestamp);
}
