using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Dispatch.Models;

namespace GreatGalaxy.Dispatch.Services
{
    public interface IRouteService
    {
        DeliveryRoute CreateRoute(Address origen, List<Address> checkpoints, Address destination);

        bool AddCheckpoint(RouteId routeId, Address checkpoint);

        DeliveryRoute GetRoute(RouteId routeId);
    
        IEnumerable<DeliveryRoute> GetAllRoutes();
    }
}
