using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Reporting.Models
{
    public record DeliveryEvent
    (
        Guid EventId,
        string EventType,
        DateTime Timestamp,
        TimeSpan Duration,
        int? RelatedCheckpoint,
        string Description);
}
