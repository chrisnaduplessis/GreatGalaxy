using GreatGalaxy.Reporting.Models;

namespace GreatGalaxy.Reporting.DomainModel
{
    public record Checkpoint(Location Location, DateTime? Arrived, List<DeliveryEvent> Events);
}
