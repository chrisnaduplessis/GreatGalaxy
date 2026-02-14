using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Dispatch.Models;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Entities.Location;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Dispatch.Repositories
{
    public class RouteRepository : BaseRepository<RouteEntity>, IRouteRepository
    {
        public DeliveryRoute Create(DeliveryRoute route)
        {
            var routeEntity = MapToRouteEntity(route);
            routeEntity.Id = this.collection.Insert(routeEntity);
            return MapToRoute(routeEntity);
        }

        public DeliveryRoute Get(RouteId routeId)
        {
            return MapToRoute(this.collection.FindById(routeId.Value));
        }

        public IEnumerable<DeliveryRoute> GetAll()
        {
            return this.collection.FindAll().Select(MapToRoute);
        }

        public bool Update(DeliveryRoute route)
        {
            return this.collection.Update(MapToRouteEntity(route));
        }

        private static RouteEntity MapToRouteEntity(DeliveryRoute route)
        {
            return new RouteEntity
            {
                Id = route.Id.HasValue ? route.Id.Value.Value : 0,
                Origen = MapToLocationEntity(route.Origen),
                Checkpoints = route.Checkpoints.Select(MapToLocationEntity),
                Destination = MapToLocationEntity(route.Destination),
                Distance = route.Distance
            };
        }

        private static DeliveryRoute MapToRoute(RouteEntity routeEntity)
        {
            var route = new DeliveryRoute(new RouteId(routeEntity.Id), MapToLocation(routeEntity.Origen), routeEntity.Checkpoints.Select(MapToLocation).ToList(), MapToLocation(routeEntity.Destination), routeEntity.Distance);
            return route;
        }

        private static LocationEntity MapToLocationEntity(Location location)
        {
            return new LocationEntity
            {
                Line1 = location.Address.Line1,
                Line2 = location.Address.Line2,
                Line3 = location.Address.Line3,
                Location = new GPSCoordinatesEntity { Latitude = location.Address.GPSCoordinates.Latitude, Longitude = location.Address.GPSCoordinates.Longitude },
                CelestialBody = location.Address.CelestialBody.Name,
                CelestialBodyPositionInSpace = new SpacePositionEntity { Position = location.Address.CelestialBody.Position.Position }
            };
        }

        private static Location MapToLocation(LocationEntity locationEntity)
        {
            var location = new Location(
                new LocationId(locationEntity.Id),
                new Address(
                    locationEntity.Line1,
                    locationEntity.Line2,
                    locationEntity.Line3,
                    new GPSCoordinates(locationEntity.Location.Latitude, locationEntity.Location.Longitude),
                    new CelestialBody(locationEntity.CelestialBody, new SpacePosition(locationEntity.CelestialBodyPositionInSpace.Position))));
            return location;
        }
    }
}
