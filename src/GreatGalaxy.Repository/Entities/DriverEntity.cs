namespace GreatGalaxy.Repository.Entities
{
    public class DriverEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Retired { get; set; }

        public List<int> ApprovedToDriveVehicles { get; set; } = new List<int>();
    }
}
