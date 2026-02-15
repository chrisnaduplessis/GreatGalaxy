namespace GreatGalaxy.Dispatch.Requests
{
    public record CreateRouteRequest(
        string OriginLine1,
        string OriginLine2,
        string OriginLine3,
        double OriginLatitude,
        double OriginLongitude,
        string OriginCelestialBody,
        string OriginCelestialBodyPosition,
        List<AddCheckpointRequest> Checkpoints,
        string DestinationLine1,
        string DestinationLine2,
        string DestinationLine3,
        double DestinationLatitude,
        double DestinationLongitude,
        string DestinationCelestialBody,
        string DestinationCelestialBodyPosition);
}
