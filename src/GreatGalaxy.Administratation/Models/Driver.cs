using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Models
{
    public class Driver
    {
        public HashSet<VehicleId> ApprovedVehicles { get; private set; }

        public DriverId? Id { get; }

        public string Name { get; private set; }

        public DateTime? Retired { get; private set; }

        public Driver(DriverId? id, string name, HashSet<VehicleId> approvedVehicles, DateTime? retired)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Driver name is required", nameof(name));

            Id = id;
            Name = name;
            Retired = retired;
            ApprovedVehicles = approvedVehicles ?? new HashSet<VehicleId>();
        }

        public void ApproveVehicle(VehicleId vehicleId)
        {
            if (!ApprovedVehicles.Add(vehicleId))
                throw new InvalidOperationException("Driver is already approved for this vehicle.");
        }

        public void RevokeVehicleApproval(VehicleId vehicleId)
        {
            ApprovedVehicles.Remove(vehicleId);
        }

        public bool CanDrive(VehicleId vehicleId)
            => ApprovedVehicles.Contains(vehicleId);

        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Driver name is required", nameof(newName));

            Name = newName;
        }

        public Driver Resign()
        {
            if (Retired.HasValue)
            {
                throw new InvalidOperationException("Driver is already resigned.");
            }

            Retired = DateTime.UtcNow;
            return this;
        }

        public Driver Reactivate()
        {
            if (!Retired.HasValue)
            {
                throw new InvalidOperationException("Driver is already active.");
            }

            Retired = null;
            return this;
        }
    }
}
