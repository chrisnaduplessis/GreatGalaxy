namespace GreatGalaxy.Repository.Entities.Location
{
    public class LocationEntity : IEntity
    {
        public int Id { get; set; }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string CelestialBody { get; set; }

        public GPSCoordinatesEntity Location { get; set; }

        public SpacePositionEntity CelestialBodyPositionInSpace { get; set; }
    }
}
