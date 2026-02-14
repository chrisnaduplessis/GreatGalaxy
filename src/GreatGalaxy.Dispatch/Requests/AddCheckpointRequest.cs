namespace GreatGalaxy.Dispatch.Requests
{
    public record AddCheckpointRequest(string Line1, string Line2, string Line3, double Latitude, double Longitude, string CelestialBody, string CelestialBodyPosition);
}
