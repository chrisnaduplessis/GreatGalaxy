using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Dispatch.Models;
using GreatGalaxy.Dispatch.Repositories;

namespace GreatGalaxy.Dispatch.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository deliveryRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            this.deliveryRepository = deliveryRepository;
        }

        public Delivery CreateDelivery(DriverId driverId, VehicleId vehicleId, RouteId routeId)
        {
            var delivery = new Delivery(routeId, driverId, vehicleId);
            return this.deliveryRepository.Create(delivery);
        }

        public bool CancelDelivery(DeliveryId deliveryId)
        {
            var delivery = this.deliveryRepository.Get(deliveryId);
            if (delivery == null)
            {
                throw new ArgumentException($"Delivery with ID {deliveryId} not found.");
            }

            delivery.Cancle();

            return this.deliveryRepository.Update(delivery);
        }

        

        public IEnumerable<Delivery> GetAllDeliveries()
        {
            return this.deliveryRepository.GetAll();
        }

        public Delivery GetDelivery(DeliveryId deliveryId)
        {
            return this.deliveryRepository.Get(deliveryId);
        }

        public bool ProcessDeliveryEvent(DeliveryId deliveryId, DeliveryEvent deliveryEvent)
        {
            var delivery = this.deliveryRepository.Get(deliveryId);
            if (delivery == null)
            {
                throw new ArgumentException($"Delivery with ID {deliveryId} not found.");
            }

            delivery.AddEvent(deliveryEvent);

            return this.deliveryRepository.Update(delivery);
        }
    }
}
