using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Administration.Repositories
{
    public interface IDriverRepository : IBaseRepository<DriverEntity>
    {
        /// <summary>
        /// Create new driver
        /// </summary>
        /// <param name="driver">Driver</param>
        /// <returns></returns>
        public Driver Create(Driver driver);

        /// <summary>
        /// Update existing driver
        /// </summary>
        /// <param name="driver">Driver</param>
        /// <returns></returns>
        public bool Update(Driver driver);

        /// <summary>
        /// Get driver by id
        /// </summary>
        /// <param name="driverId">Driver id</param>
        /// <returns></returns>
        public Driver Get(DriverId driverId);


        /// <summary>
        /// Get all drivers, including both active and resigned drivers. This method will return a list of all driver records in the database, regardless of their active status.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Driver> GetAll();

        /// <summary>
        /// Get all active drivers, which means only drivers that are currently active and not resigned. This method will return a list of driver records that are marked as active in the database, excluding any drivers that have been resigned.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Driver> GetAllActive();
    }
}
