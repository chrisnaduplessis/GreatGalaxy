using GreatGalaxy.Common.Utilities;
using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Dispatch.Models;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Dispatch.Repositories
{
    public class DeliveryRepository : BaseRepository<DeliveryEntity>, IDeliveryRepository
    {
        public Delivery Create(Delivery delivery)
        {
            var deliveryEntity = MapToDeliveryEntity(delivery);
            deliveryEntity.Id = this.collection.Insert(deliveryEntity);
            return MapToDelivery(deliveryEntity);

        }

        public Delivery Get(DeliveryId deliveryId)
        {
            return MapToDelivery(this.collection.FindById(deliveryId.Value));
        }

        public IEnumerable<Delivery> GetAll()
        {
            return this.collection.FindAll().Select(MapToDelivery);
        }

        public bool Update(Delivery delivery)
        {
            return this.collection.Update(MapToDeliveryEntity(delivery));
        }

        private static DeliveryEntity MapToDeliveryEntity(Delivery delivery)
        {
            return new DeliveryEntity
            {
                Id = delivery.Id.HasValue ? delivery.Id.Value.Value : 0,
                Departed = delivery.Departed,
                Arrived = delivery.Arrived,
                DriverId = delivery.DriverId.Value,
                RouteId = delivery.RouteId.Value,
                VehicleId = delivery.VehicleId.Value,
                Status = delivery.Status,
                CheckpointReached = delivery.CheckpointsReached.Select(cr => new CheckpointReachedEntity
                {
                    LocationId = cr.LocationId.Value,
                    Timestamp = cr.Timestamp
                }).ToList(),
                Events = delivery.Events.Select(e => new DeliveryEventEntity
                {
                    Id = IdGenerator.GenerateId(),
                    EventId = e.EventId,
                    EventType = e.EventType,
                    Timestamp = e.Timestamp,
                    Duration = e.Duration,
                    Description = e.Description,
                    RelatedCheckpoint = e.RelatedCheckpoint?.Value,
                }).ToList()
            };
        }

        private static Delivery MapToDelivery(DeliveryEntity deliveryEntity)
        {
            return new Delivery(
                new DeliveryId(deliveryEntity.Id),
                new RouteId(deliveryEntity.RouteId),
                new DriverId(deliveryEntity.DriverId),
                new VehicleId(deliveryEntity.VehicleId),
                deliveryEntity.Events.Select(e => new DeliveryEvent(e.EventId, e.EventType, e.Timestamp, e.Duration, e.RelatedCheckpoint.HasValue ? new LocationId(e.RelatedCheckpoint.Value) : null, e.Description)).ToList(),
                deliveryEntity.Departed,
                deliveryEntity.Arrived,
                deliveryEntity.Status
            );
        }
    }
}
