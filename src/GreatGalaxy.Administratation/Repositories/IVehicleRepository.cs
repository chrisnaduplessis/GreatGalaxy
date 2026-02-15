using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Administration.Repositories
{
    public interface IVehicleRepository : IBaseRepository<VehicleEntity>
    {
        /// <summary>
        /// Create new vehicle in the repository. Returns the created vehicle with its assigned ID.
        /// </summary>
        /// <param name="vehicle">Vehicle</param>
        /// <returns></returns>
        Vehicle Create(Vehicle vehicle);

        /// <summary>
        /// Update vehicle description in the repository. Returns true if the update was successful, false if the vehicle was not found.
        /// </summary>
        /// <param name="entity">Updated Vehicle</param>
        /// <returns></returns>
        bool Update(Vehicle entity);

        /// <summary>
        /// Get vehicle by its ID. Returns null if the vehicle was not found.
        /// </summary>
        /// <param name="id">Vehicle id</param>
        /// <returns></returns>
        Vehicle Get(VehicleId id);

        /// <summary>
        /// Get all vehicles in the repository. Returns an empty collection if no vehicles are found.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Vehicle> GetAll();

    }
}
