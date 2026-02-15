using GreatGalaxy.Common.ValueTypes.Delivery;

namespace GreatGalaxy.Repository.Entities
{
    public class DeliveryEntity : IEntity
    {
        public int Id { get; set; }

        public int RouteId { get; set; }

        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public List<CheckpointReachedEntity> CheckpointReached { get; set; }

        public DeliveryStatus Status { get; set; }

        public DateTime? Departed { get; set; }

        public DateTime? Arrived { get; set; }

        public List<DeliveryEventEntity> Events { get; set; }
    }
}
