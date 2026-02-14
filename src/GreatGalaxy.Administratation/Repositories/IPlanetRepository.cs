using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Repository.Entities.Location;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Administration.Repositories
{
    public interface IPlanetRepository : IBaseRepository<PlanetEntity>
    {
        /// <summary>
        /// Create new planet
        /// </summary>
        /// <param name="planet">Planet</param>
        /// <returns></returns>
        Planet Create(Planet planet);

        /// <summary>
        /// Update existing planet
        /// This update is used for all updates of planet, including adding and removing countries
        /// It should be updated to only update the affected countries, but we won't do that for now
        /// </summary>
        /// <param name="planet">Planet</param>
        /// <returns></returns>
        bool Update(Planet planet);

        /// <summary>
        /// Get planet by id
        /// </summary>
        /// <param name="planetId">Planet id</param>
        /// <returns></returns>
        Planet Get(PlanetId planetId);
    
        IEnumerable<Planet> GetAll();

        Planet GetByName(string name);
    }
}
