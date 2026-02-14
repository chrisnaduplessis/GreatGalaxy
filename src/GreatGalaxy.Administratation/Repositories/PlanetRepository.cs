using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Repository.Entities.Location;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Administration.Repositories
{
    public class PlanetRepository : BaseRepository<PlanetEntity>, IPlanetRepository
    {
        public PlanetRepository() { }

        public Planet Create(Planet planet)
        {
            var entity = MapToPlanetEntity(planet);
            var id = this.collection.Insert(entity);
            return new Planet(
                new PlanetId(id),
                planet.Name,
                null);
        }

        public Planet Get(PlanetId planetId)
        {
            return MapToPlanet(this.collection.FindById(planetId.Value));
        }

        public IEnumerable<Planet> GetAll()
        {
            return this.collection.FindAll().Select(MapToPlanet);
        }

        public Planet GetByName(string name)
        {
            return MapToPlanet(this.collection.FindOne(e => e.Name == name));
        }

        public bool Update(Planet planet)
        {
            return this.collection.Update(MapToPlanetEntity(planet));
        }

        private static Planet MapToPlanet(PlanetEntity entity)
        {
            return new Planet(
                new PlanetId(entity.Id),
                entity.Name,
                entity.Countries.Select(MapToCountry).ToList());
        }

        public static PlanetEntity MapToPlanetEntity(Planet planet)
        {
            return new PlanetEntity
            {
                Id = planet.Id.HasValue ? planet.Id.Value.Value : 0,
                Name = planet.Name,
                Countries = planet.Countries.Select(MapToCountryEntity).ToList()
            };
        }

        public static CountryEntity MapToCountryEntity(Country country)
        {
            return new CountryEntity
            {
                Id = country.Id.HasValue ? country.Id.Value.Value : 0,
                Name = country.Name,
                Locations = country.Locations.Select(MapToLocationEntity).ToList()
            };
        }

        public static Country MapToCountry(CountryEntity entity)
        {
            return new Country(
                new CountryId(entity.Id),
                entity.Name,
                entity.Locations.Select(MapToLocation).ToList());
        }

        public static LocationEntity MapToLocationEntity(Location location)
        {
            return new LocationEntity
            {
                Id = location.Id.HasValue ? location.Id.Value.Value : 0,
                Line1 = location.Address.AddressLine1,
                Line2 = location.Address.AddressLine2,
                City = location.Address.City,
                PostalCode = location.Address.PostalCode,
            };
        }

        public static Location MapToLocation(LocationEntity entity)
        {
            return new Location(
                new LocationId(entity.Id),
                new Address(
                    entity.Line1,
                    entity.Line2,
                    entity.City,
                    entity.PostalCode));
        }
    }
 }
