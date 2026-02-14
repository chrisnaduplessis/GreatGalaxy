using LiteDB;

namespace GreatGalaxy.Repository.Entities
{
    public class VehicleEntity : IEntity
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public double WeightAllowanceKg { get; set; }

        public double VolumeAllowanceM3 { get; set; }

        public double SpeedMetersPerSecond { get; set; }

        public DateTime? WhenScrapped { get; set; }
    }
}
