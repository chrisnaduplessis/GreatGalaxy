using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Reporting.Repositories
{
    public interface IRouteRepository: IBaseRepository<RouteEntity>
    {
        RouteEntity Get(int routeId);
    }
}
