using GreatGalaxy.Common.ValueTypes.Event;

namespace GreatGalaxy.Repository.Entities
{
    public class DeliveryEventEntity
    {
        public int Id { get; set; }

        public Guid EventId { get; set; }

        public EventType EventType { get; set; }

        public DateTime Timestamp { get; set; }

        public TimeSpan Duration { get; set; }

        public int? RelatedCheckpoint { get; set; }

        public string Description { get; set; }
    }
}
