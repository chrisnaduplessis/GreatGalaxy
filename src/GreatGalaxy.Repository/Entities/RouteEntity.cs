using GreatGalaxy.Repository.Entities.Location;

namespace GreatGalaxy.Repository.Entities
{
    public class RouteEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public LocationEntity Origen { get; set; }

        public IEnumerable<LocationEntity> Checkpoints { get; set; }

        public LocationEntity Destination { get; set; }

        public double Distance { get; set; }
    }
}
