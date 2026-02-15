namespace GreatGalaxy.Administration.Requests.Vehicle
{
    public record CreateVehicleRequest(
    string Make,
    string Model,
    string Description,
    double WeightAllowanceKg,
    double VolumeAllowanceM3,
    double SpeedMetersPerSecond);
}
