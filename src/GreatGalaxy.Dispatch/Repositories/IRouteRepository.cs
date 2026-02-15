using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Dispatch.Models;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Dispatch.Repositories
{
    public interface IRouteRepository: IBaseRepository<RouteEntity>
    {
        DeliveryRoute Create(DeliveryRoute route);

        bool Update(DeliveryRoute route);

        DeliveryRoute Get(RouteId routeId);

        IEnumerable<DeliveryRoute> GetAll();
    }
}
