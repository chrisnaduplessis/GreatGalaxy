using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Services
{
    public interface IPlanetService
    {
        /// <summary>
        /// Create new planet
        /// </summary>
        /// <param name="name">Planet name</param>
        /// <returns></returns>
        Planet Create(string name);
    
        /// <summary>
        /// Get planet by id
        /// </summary>
        /// <param name="planetId">Planet id</param>
        /// <returns></returns>
        Planet Get(PlanetId planetId);

        Planet GetByName(string name);

        IEnumerable<Planet> GetAll();

        Planet AddCountry(PlanetId planetId, string countryName);

        Planet AddLocation(PlanetId planetId, CountryId countryId, Address address);
    }
}
