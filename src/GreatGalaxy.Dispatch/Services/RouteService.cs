using GreatGalaxy.Common.Utilities;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Dispatch.Models;
using GreatGalaxy.Dispatch.Repositories;

namespace GreatGalaxy.Dispatch.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            this.routeRepository = routeRepository;
        }

        public DeliveryRoute CreateRoute(Address origen, List<Address> checkpoints, Address destination)
        {
            return this.routeRepository.Create(
                new DeliveryRoute(
                    null,
                    new Location(new LocationId(IdGenerator.GenerateId()), origen), 
                    checkpoints.Select(_ => new Location(new LocationId(IdGenerator.GenerateId()), _)).ToList(),
                    new Location(new LocationId(IdGenerator.GenerateId()), destination), 
                    CalculateDistance(origen, destination)));
        }

        public IEnumerable<DeliveryRoute> GetAllRoutes()
        {
            return this.routeRepository.GetAll();
        }

        public DeliveryRoute GetRoute(RouteId routeId)
        {
            return this.routeRepository.Get(routeId);
        }

        public bool AddCheckpoint(RouteId routeId, Address checkpoint)
        {
            var route = this.routeRepository.Get(routeId);
            if (route == null)
            {
                throw new Exception($"Route with id {routeId.Value} not found");
            }
            route.AddCheckpoint(new Location(new LocationId(IdGenerator.GenerateId()), checkpoint));

            return this.routeRepository.Update(route);
        }

        public static double CalculateDistance(Address origen, Address destination)
        {
            // Some fancy login that calculates the distance between two locations
            Random random = new Random();
            if (origen.CelestialBody.Name == destination.CelestialBody.Name)
            {
                return random.NextDouble() * 10;
            }

            return random.NextDouble() * 100_000;
        }
    }
}
