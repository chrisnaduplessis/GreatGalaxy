using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Administration.Repositories
{
    public class DriverRepository : BaseRepository<DriverEntity>, IDriverRepository
    {
        /// <inheritdoc/>
        public Driver Create(Driver driver)
        {
            var driverEntity = MapToDriverEntity(driver);
            driverEntity.Id = collection.Insert(driverEntity);

            return MapToDriver(driverEntity);
        }

        /// <inheritdoc/>
        public Driver Get(DriverId driverId)
        {
            return MapToDriver(collection.FindById(driverId.Value));
        }

        /// <inheritdoc/>
        public bool Update(Driver driver)
        {
            return collection.Update(MapToDriverEntity(driver));
        }

        /// <inheritdoc/>
        public IEnumerable<Driver> GetAll()
        {
            return collection.FindAll().Select(_ => MapToDriver(_));
        }

        /// <inheritdoc/>
        public IEnumerable<Driver> GetAllActive()
        {
            return collection.Find(_ => !_.Retired.HasValue).Select(_ => MapToDriver(_));
        }

        private static DriverEntity MapToDriverEntity(Driver driver)
        {
            return new DriverEntity
            {
                Id = driver.Id.HasValue ? driver.Id.Value.Value : 0,
                Name = driver.Name,
                ApprovedToDriveVehicles = driver.ApprovedVehicles.Select(v => v.Value).ToList()
            };
        }

        private static Driver MapToDriver(DriverEntity driverEntity)
        {
            var approvedVehicles = driverEntity.ApprovedToDriveVehicles.Select(v => new VehicleId(v)).ToHashSet();
            var driver = new Driver(new DriverId(driverEntity.Id), driverEntity.Name, approvedVehicles);
            return driver;
        }
    }
}
