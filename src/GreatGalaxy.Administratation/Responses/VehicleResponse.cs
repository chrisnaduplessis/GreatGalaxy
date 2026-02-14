using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Responses
{
    public record VehicleResponse
    (
        int Id,
        VehicleTypeResponse Type,
        string Description,
        double WeightAllowance,
        double VolumeAllowance,
        double MaxSpeed,
        DateTime? WhenScrapped
    );
}
