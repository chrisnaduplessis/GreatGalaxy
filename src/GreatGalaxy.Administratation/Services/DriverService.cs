using GreatGalaxy.Administration.Models;
using GreatGalaxy.Administration.Repositories;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using System.Xml.Linq;

namespace GreatGalaxy.Administration.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        public Driver Create(string name)
        {
            var driver = new Driver(null, name, new HashSet<VehicleId>());
            return this.driverRepository.Create(driver);
        }

        public Driver Rename(DriverId driverId, string name)
        {
            var driver = this.driverRepository.Get(driverId);
            if (driver == null)
            {
                throw new InvalidOperationException($"Driver with id {driverId} not found.");
            }

            driver.Rename(name);
            return driver;
        }

        public Driver Retire(DriverId driverId)
        {
            var driver = this.driverRepository.Get(driverId);
            if (driver == null)
            {
                throw new InvalidOperationException($"Driver with id {driverId} not found.");
            }

            driver.Resign();
            return driver;
        }

        public Driver Reactivate(DriverId driverId)
        {
            var driver = this.driverRepository.Get(driverId);
            if (driver == null)
            {
                throw new InvalidOperationException($"Driver with id {driverId} not found.");
            }

            driver.Reactivate();
            return driver;
        }

        public Driver ApproveVehicle(DriverId driverId, VehicleId vehicleId)
        {
            var driver = this.driverRepository.Get(driverId);

            if (driver == null)
            {
                throw new InvalidOperationException($"Driver with id {driverId} not found.");
            }

            driver.ApproveVehicle(vehicleId);
            if (this.driverRepository.Update(driver))
            {
                return driver;
            }
            else
            {
                throw new InvalidOperationException($"Failed to update driver with id {driverId}.");
            }
        }

        public Driver RevokeVehicleApproval(DriverId driverId, VehicleId vehicleId)
        {
            var driver = this.driverRepository.Get(driverId);

            if (driver == null)
            {
                throw new InvalidOperationException($"Driver with id {driverId} not found.");
            }

            driver.RevokeVehicleApproval(vehicleId);
            if (this.driverRepository.Update(driver))
            {
                return driver;
            }
            else
            {
                throw new InvalidOperationException($"Failed to update driver with id {driverId}.");
            }
        }

        public Driver Get(DriverId driverId)
        {
            return this.driverRepository.Get(driverId);
        }

        public IEnumerable<Driver> GetAll()
        {
            return this.driverRepository.GetAll();
        }

        public IEnumerable<Driver> GetAllActive()
        {
            return this.driverRepository.GetAllActive();
        }

    }
}
