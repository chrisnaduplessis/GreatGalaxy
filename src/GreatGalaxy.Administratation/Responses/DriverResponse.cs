namespace GreatGalaxy.Administration.Responses
{
    public record DriverResponse
    (
        int Id,
        string Name,
        DateTime? Retired,
        IEnumerable<int> ApprovedVehicleIds
    );
}
