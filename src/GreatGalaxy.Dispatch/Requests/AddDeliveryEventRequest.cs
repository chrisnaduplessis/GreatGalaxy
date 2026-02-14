using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Dispatch.Requests
{
    public record AddDeliveryEventRequest(
        int DeliveryId,
        EventType eventType,
        DateTime Timestamp,
        TimeSpan Duration,
        int? LocationId,
        string Description);
}
