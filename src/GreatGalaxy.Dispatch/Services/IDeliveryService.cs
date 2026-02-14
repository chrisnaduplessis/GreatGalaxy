using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Dispatch.Models;

namespace GreatGalaxy.Dispatch.Services
{
    public interface IDeliveryService
    {
        Delivery CreateDelivery(DriverId driver, VehicleId vehicleId, RouteId routeId);
    
        Delivery GetDelivery(DeliveryId deliveryId);

        IEnumerable<Delivery> GetAllDeliveries();

        bool CancelDelivery(DeliveryId deliveryId);

        bool ProcessDeliveryEvent(DeliveryId deliveryId, DeliveryEvent deliveryEvent);
    }
}
