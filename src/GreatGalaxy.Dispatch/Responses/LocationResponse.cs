namespace GreatGalaxy.Dispatch.Responses
{
    public record LocationResponse(int LocationId, string Line1, string Line2, string Line3, double Latitude, double Longitude, string CelestialBody, string CelestialBodyPosition);
}
