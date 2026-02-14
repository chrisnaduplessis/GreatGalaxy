using GreatGalaxy.Administration.DomainItems;
using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;
using LiteDB;

namespace GreatGalaxy.Administration.Repositories
{
    public class VehicleRepository : BaseRepository<VehicleEntity>, IVehicleRepository
    {
        /// <inheritdoc/>
        public Vehicle Create(Vehicle vehicle)
        {
            var vehicleEntity = MapToVehicleEntity(vehicle);
            vehicleEntity.Id = this.collection.Insert(vehicleEntity);
            return MapToVehicle(vehicleEntity);
        }

        /// <inheritdoc/>
        public Vehicle Get(VehicleId id)
        {
            return MapToVehicle(collection.FindById(id.Value));
        }

        /// <inheritdoc/>
        public IEnumerable<Vehicle> GetAll()
        {
            var vehicleEntities = collection.FindAll();
            return vehicleEntities.Select(_ => MapToVehicle(_));
        }

        /// <inheritdoc/>
        public bool Update(Vehicle vehicle)
        {
            return collection.Update(MapToVehicleEntity(vehicle));
        }

        private static VehicleEntity MapToVehicleEntity(Vehicle vehicle)
        {
            return new VehicleEntity
            {
                Id = vehicle.Id.HasValue ? vehicle.Id.Value.Value : 0,
                Make = vehicle.Type.Make,
                Model = vehicle.Type.Model,
                Description = vehicle.Description,
                WeightAllowanceKg = vehicle.WeightAllowance.Value,
                VolumeAllowanceM3 = vehicle.VolumeAllowance.Value,
                SpeedMetersPerSecond = vehicle.MaxSpeed.Value,
                WhenScrapped = vehicle.WhenScrapped,
            };
        }

        private static Vehicle MapToVehicle(VehicleEntity vehicleEntity)
        {
            return new Vehicle(
                new VehicleId(vehicleEntity.Id),
                new VehicleType(vehicleEntity.Make, vehicleEntity.Model),
                vehicleEntity.Description,
                new WeightKg(vehicleEntity.WeightAllowanceKg),
                new VolumeM3(vehicleEntity.VolumeAllowanceM3),
                new SpeedMetersPerSecond(vehicleEntity.SpeedMetersPerSecond),
                vehicleEntity.WhenScrapped);
        }
    }
}
