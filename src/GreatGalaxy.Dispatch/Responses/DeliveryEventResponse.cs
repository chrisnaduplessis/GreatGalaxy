using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Dispatch.Responses
{
    public record DeliveryEventResponse(Guid EventId, EventType EventType, DateTime Timestamp, TimeSpan Duration, int? RelatedCheckpoint, string Description);
}
