using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Reporting.Repositories
{
    public interface IDriverRepository : IBaseRepository<DriverEntity>
    {
        /// <summary>
        /// Get driver by id
        /// </summary>
        /// <param name="driverId">Driver id</param>
        /// <returns></returns>
        public DriverEntity Get(int driverId);
    }
}
