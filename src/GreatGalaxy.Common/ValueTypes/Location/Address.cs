namespace GreatGalaxy.Common.ValueTypes.Location
{
    public record struct Address(string Line1, string Line2, string Line3, GPSCoordinates GPSCoordinates, CelestialBody CelestialBody);
}
