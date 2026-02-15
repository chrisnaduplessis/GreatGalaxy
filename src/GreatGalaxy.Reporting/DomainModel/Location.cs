namespace GreatGalaxy.Reporting.DomainModel
{
    public record Location(
        int Id,
        string Line1,
        string Line2,
        string Line3,
        string CelestialBody,
        double Latitude,
        double Longitude,
        string CelestialBodyPositionInSpace);
}
