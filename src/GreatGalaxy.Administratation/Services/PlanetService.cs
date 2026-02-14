using GreatGalaxy.Administration.Models;
using GreatGalaxy.Administration.Repositories;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository planetRepository;
        private readonly Random random = new Random();

        public PlanetService(IPlanetRepository planetRepository)
        {
            this.planetRepository = planetRepository;
        }

        public Planet Create(string name)
        {
            return this.planetRepository.Create(new Planet(null, name, new List<Country>()));
        }

        public Planet AddCountry(PlanetId planetId, string countryName)
        {
            var planet = this.planetRepository.Get(planetId);
            if (planet == null)
            {
                throw new Exception($"Planet with id {planetId} not found.");
            }

            // We should check if the country already exists here, but for simplicity we will skip this step.
            planet.AddCountry(new Country(new CountryId(this.GenerateId()), countryName, new List<Location>()));

            if (this.planetRepository.Update(planet))
            {
                return planet;
            }
            else
            {
                throw new Exception($"Failed to update planet with id {planetId}.");
            }

        }

        public Planet AddLocation(PlanetId planetId, CountryId countryId, Address address)
        {
            var planet = this.planetRepository.Get(planetId);
            if (planet == null)
            {
                throw new Exception($"Planet with id {planetId} not found.");
            }

            var country = planet.Countries.FirstOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                throw new Exception($"Country with id {countryId} not found in planet with id {planetId}.");
            }

            // We should check if the location already exists here, but for simplicity we will skip this step.
            country.AddLocation(new Location(new LocationId(this.GenerateId()), address));
            this.planetRepository.Update(planet);
            return planet;
        }



        public Planet Get(PlanetId planetId)
        {
            return this.planetRepository.Get(planetId);
        }

        public Planet GetByName(string name)
        {
            return this.planetRepository.GetByName(name);
        }

        public IEnumerable<Planet> GetAll()
        {
             return this.planetRepository.GetAll();
        }

        private int GenerateId()
        {
            // Assume that the id is always going to be unique, wouldn't use this in a real application, but for simplicity we will use this approach here.
            return random.Next(1, int.MaxValue);
        }
    }
}
