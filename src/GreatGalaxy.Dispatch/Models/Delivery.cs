using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Dispatch.Models
{
    public class Delivery
    {
        public DeliveryId? Id { get; }

        public RouteId RouteId { get; }

        public DateTime? Departed { get; private set; }

        public DateTime? Arrived { get; private set; }

        public DriverId DriverId { get; }

        public VehicleId VehicleId { get; }

        public List<DeliveryEvent> Events { get; } = new List<DeliveryEvent>();

        public DeliveryStatus Status { get; private set; } = DeliveryStatus.Pending;

        public List<CheckpointReached> CheckpointsReached { get; } = new List<CheckpointReached>();

        public Delivery(RouteId routeId, DriverId driverId, VehicleId vehicleId)
        {
            RouteId = routeId;
            DriverId = driverId;
            VehicleId = vehicleId;
        }

        public Delivery(DeliveryId id, RouteId routeId, DriverId driverId, VehicleId vehicleId, List<DeliveryEvent> events, DateTime? departed, DateTime? arrived, DeliveryStatus status)
        {
            Id = id;
            RouteId = routeId;
            DriverId = driverId;
            VehicleId = vehicleId;
            Events = events;
            Departed = departed;
            Arrived = arrived;
            Status = status;
        }

        public void AddEvent(DeliveryEvent deliveryEvent)
        {
            // There really should be locking here to ensure that we don't process events more than once for the same delivery
            // This could be implemented using IDistributedLock configured with Redis for example
            // Some thought should also be given to what will happen if events are process out of order
            if (Events.Any(e => e.EventId == deliveryEvent.EventId))
            {
                // Event has already been processed, ignore it
                // Most messaging systems will guarantee at least once delivery, so we need to be able to handle duplicate events gracefully
                return;
            }
            Events.Add(deliveryEvent);
            ProcessEvent(deliveryEvent);
        }

        public void ProcessEvent(DeliveryEvent deliveryEvent)
        {
            // This is a very simple implementation, in a real application we would have more complex logic here
            switch (deliveryEvent.EventType)
            {
                case EventType.VehicleDeparture:
                    Status = DeliveryStatus.InTransit;
                    Departed = deliveryEvent.Timestamp;
                    break;
                case EventType.DestinationReached:
                    Status = DeliveryStatus.Delivered;
                    Arrived = deliveryEvent.Timestamp;
                    break;
                case EventType.CheckpointReached:
                    if (deliveryEvent.RelatedCheckpoint.HasValue)
                    {
                        CheckpointsReached.Add(new CheckpointReached(deliveryEvent.RelatedCheckpoint.Value, deliveryEvent.Timestamp));
                    }
                    break;
                case EventType.Disruption:
                case EventType.Disaster:
                    Status = DeliveryStatus.Delayed;
                    break;
                case EventType.CompleteAndUtterFailure:
                    this.Cancle();
                    break;
                default:
                    // For other event types, we might not change the status
                    break;
            }
        }

        public void Cancle()
        {
            Status = DeliveryStatus.Cancelled;
        }
    }
}
