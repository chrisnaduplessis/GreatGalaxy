using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Models
{
    public record Vehicle(
        int Id,
        VehicleType Type,
        string Description);

}
