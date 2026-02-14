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
        List<(string Line1, string Line2, string Line3, double Latitude, double Longitude, string CelestialBody, string CelestialBodyPosition)> Checkpoints,
        string DestinationLine1,
        string DestinationLine2,
        string DestinationLine3,
        double DestinationLatitude,
        double DestinationLongitude,
        string DestinationCelestialBody,
        string DestinationCelestialBodyPosition);
}
