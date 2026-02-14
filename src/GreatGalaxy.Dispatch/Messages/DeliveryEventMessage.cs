using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Dispatch.Messages
{
    // It would have been better to split events into separate types instead of having a single event type with an enum to indicate the type, but for simplicity we'll just use a single type for now
    public record DeliveryEventMessage(Guid Id, DeliveryId DeliveryId, EventType eventType, DateTime Timestamp, TimeSpan Duration, LocationId? LocationId, string Description);
}
