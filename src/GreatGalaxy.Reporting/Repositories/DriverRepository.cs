using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Reporting.Repositories
{
    public class DriverRepository : BaseRepository<DriverEntity>, IDriverRepository
    {
        /// <inheritdoc/>
        public DriverEntity Get(int driverId)
        {
            return collection.FindById(driverId);
        }
    }
}
