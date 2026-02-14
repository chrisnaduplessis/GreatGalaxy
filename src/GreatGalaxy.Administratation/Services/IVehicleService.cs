using GreatGalaxy.Administration.DomainItems;
using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Services
{
    public interface IVehicleService
    {
        /// <summary>
        /// Create a new vehicle with the given parameters and save it to the repository. Returns the created vehicle with its assigned ID.
        /// </summary>
        /// <param name="make">Vehicle make</param>
        /// <param name="model">Vehicle model</param>
        /// <param name="description">Description of vehicle</param>
        /// <param name="weightAllowanceKg">How much weight can the vehicle handle?</param>
        /// <param name="volumeAllowanceM3">How much stuff can I load?</param>
        /// <param name="maxSpeedMetersPerSecond">How fast can this thing possibly go?</param>
        /// <returns></returns>
        Vehicle Create(string make,
            string model,
            string description,
            double weightAllowance,
            double volumeAllowanceM3,
            double maxSpeedMetersPerSecond);

        /// <summary>
        /// Update an existing vehicle
        /// </summary>
        /// <param name="id">Vehicle id</param>
        /// <param name="description">Description of vehicle</param>
        /// <returns></returns>
        Vehicle Update(int id,
            string description);

        /// <summary>
        /// Get vehicle by id. Returns null if not found.
        /// </summary>
        /// <param name="id">Vehicle id</param>
        /// <returns></returns>
        Vehicle Get(VehicleId id);

        /// <summary>
        /// Get all vehicles in the repository. Returns empty list if no vehicles are found.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Vehicle> GetAll();

        /// <summary>
        /// Vehicle is scrapped, meaning it is no longer in use and should be removed from the repository. If the vehicle is not found, this method does nothing.
        /// </summary>
        /// <param name="id">Vehicle id</param>
        public void Scrap(VehicleId id);
    }
}
