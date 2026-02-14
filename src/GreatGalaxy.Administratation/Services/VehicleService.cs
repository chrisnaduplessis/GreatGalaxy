using GreatGalaxy.Administration.DomainItems;
using GreatGalaxy.Administration.Repositories;
using GreatGalaxy.Common.ValueTypes;

namespace GreatGalaxy.Administration.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            ArgumentNullException.ThrowIfNull(vehicleRepository);

            this.vehicleRepository = vehicleRepository;
        }


        /// <inheritdoc/>
        public Vehicle Create(string make, string model, string description, double weightAllowanceKg, double volumeAllowanceM3, double maxSpeedMetersPerSecond)
        {
            var vehicle = new Vehicle(
                null,
                new VehicleType(make, model),
                description,
                new WeightKg(weightAllowanceKg),
                new VolumeM3(volumeAllowanceM3),
                new SpeedMetersPerSecond(maxSpeedMetersPerSecond),
                null);
            return vehicleRepository.Create(vehicle);
        }

        /// <inheritdoc/>
        public Vehicle Get(VehicleId id)
        {
            return vehicleRepository.Get(id);
        }

        /// <inheritdoc/>
        public IEnumerable<Vehicle> GetAll()
        {
            return vehicleRepository.GetAll();
        }

        /// <inheritdoc/>
        public Vehicle Update(int id,
            string description)
        {
            var existingVehicle = vehicleRepository.Get(new VehicleId(id));
            if (existingVehicle == null)
            {
                throw new InvalidOperationException($"Vehicle with id {id} does not exist.");
            }

            existingVehicle.UpdateDescription(description);
            if (vehicleRepository.Update(existingVehicle))
            {
                return existingVehicle;
            }
            else
            {
                throw new InvalidOperationException($"Failed to update vehicle with id {id}.");
            }
        }

        public void Scrap(VehicleId id)
        {
            var existingVehicle = vehicleRepository.Get(id);
            if (existingVehicle == null)
            {
                throw new InvalidOperationException($"Vehicle with id {id} does not exist.");
            }

            existingVehicle.Scrap();
        }
    }
}
