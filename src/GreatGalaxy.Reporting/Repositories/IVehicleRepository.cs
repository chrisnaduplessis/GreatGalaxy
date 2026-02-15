using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Reporting.Repositories
{
    public interface IVehicleRepository : IBaseRepository<VehicleEntity>
    {
        VehicleEntity Get(int id);
    }
}
