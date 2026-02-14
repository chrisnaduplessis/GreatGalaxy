using GreatGalaxy.Administration.DomainItems;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Services
{
    public interface IDriverService
    {
        /// <summary>
        /// Create new driver
        /// </summary>
        /// <param name="name">Driver name</param>
        /// <returns></returns>
        Driver Create(string name);

        /// <summary>
        /// Update existing driver
        /// </summary>
        /// <param name="driverId">Driver</param>
        /// <returns></returns>
        Driver Rename(DriverId driverId, string name);

        /// <summary>
        /// Set driver as resigned, which means they are no longer active and cannot be assigned to vehicles.
        /// This is a soft delete, so the driver record will still exist in the database but will be marked as inactive.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        Driver Retire(DriverId driverId);

        /// <summary>
        /// Reactivate a resigned driver, which means they will become active again and can be assigned to vehicles.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        Driver Reactivate(DriverId driverId);

        /// <summary>
        /// Add a vehicle to the list of vehicles that the driver is approved to drive. This means the driver has permission to operate the specified vehicle.
        /// </summary>
        /// <param name="driverId">Driver id</param>
        /// <param name="vehicleId">Vehicle id</param>
        /// <returns></returns>
        Driver ApproveVehicle(DriverId driverId, VehicleId vehicleId);

        /// <summary>
        /// Revoke approval to drive a specific vehicle from a driver. This means the driver will no longer have permission to operate the specified vehicle.
        /// </summary>
        /// <param name="driverId"></param>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        Driver RevokeVehicleApproval(DriverId driverId, VehicleId vehicleId);

        /// <summary>
        /// Get driver by id
        /// </summary>
        /// <param name="driverId">Driver id</param>
        /// <returns></returns>
        Driver Get(DriverId driverId);


        /// <summary>
        /// Get all drivers, including both active and resigned drivers. This method will return a list of all driver records in the database, regardless of their active status.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Driver> GetAll();

        /// <summary>
        /// Get all active drivers, which means only drivers that are currently active and not resigned. This method will return a list of driver records that are marked as active in the database, excluding any drivers that have been resigned.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Driver> GetAllActive();
    }
}
