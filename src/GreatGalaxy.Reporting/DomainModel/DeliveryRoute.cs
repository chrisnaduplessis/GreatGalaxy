using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Reporting.DomainModel;

namespace GreatGalaxy.Reporting.Models
{
    public record DeliveryRoute (
        int Id,
        Location Origen,
        List<Checkpoint> Checkpoints,
        Location Destination);
}
