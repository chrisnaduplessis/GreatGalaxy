using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Dispatch.Models
{
    public class DeliveryEvent
    {
        public Guid EventId { get; }

        public EventType EventType { get; }

        public DateTime Timestamp { get; }

        public TimeSpan Duration { get; }

        public LocationId? RelatedCheckpoint { get; }

        public string Description { get; }

        public DeliveryEvent(Guid eventId, EventType eventType, DateTime timestamp, TimeSpan duration, LocationId? relatedCheckpoint, string description)
        {
            EventId = eventId;
            EventType = eventType;
            Timestamp = timestamp;
            Duration = duration;
            RelatedCheckpoint = relatedCheckpoint;
            Description = description;
        }
    }
}
