using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Reporting.Repositories
{
    public class RouteRepository : BaseRepository<RouteEntity>, IRouteRepository
    {
        public RouteEntity Get(int routeId)
        {
            return this.collection.FindById(routeId);
        }
    }
}
