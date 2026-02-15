using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;
using LiteDB;

namespace GreatGalaxy.Reporting.Repositories
{
    public class VehicleRepository : BaseRepository<VehicleEntity>, IVehicleRepository
    {
        /// <inheritdoc/>
        public VehicleEntity Get(int id)
        {
            return this.collection.FindById(id);
        }
    }
}
