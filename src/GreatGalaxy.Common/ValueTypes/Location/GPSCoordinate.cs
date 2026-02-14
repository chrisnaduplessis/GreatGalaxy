namespace GreatGalaxy.Common.ValueTypes.Location
{
    /// <summary>
    /// GPS coordinates of location on a celestial body, such as Earth.
    /// It is assumed that some sort of similar systme exists on other celestial bodies, but the details of how it works are not yet defined.
    /// </summary>
    public record GPSCoordinates(double Latitude, double Longitude);
}
