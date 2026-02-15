using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Models
{
    public class Vehicle
    {
        public VehicleId? Id { get; }

        public VehicleType Type { get; private set; }

        public string Description { get; private set; }

        public WeightKg WeightAllowance { get; private set; }

        public VolumeM3 VolumeAllowance { get; private set; }

        public SpeedMetersPerSecond MaxSpeed { get; private set; }

        public DateTime? WhenScrapped { get; private set; }

        public Vehicle(
            VehicleId? id,
            VehicleType type,
            string description,
            WeightKg weightAllowance,
            VolumeM3 volumeAllowance,
            SpeedMetersPerSecond maxSpeed,
            DateTime? whenScrapped)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Vehicle description is required", nameof(description));

            Id = id;
            Type = type;
            Description = description;
            WeightAllowance = weightAllowance;
            VolumeAllowance = volumeAllowance;
            MaxSpeed = maxSpeed;
            WhenScrapped = whenScrapped;
        }

        public void UpdateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Vehicle description is required", nameof(description));

            Description = description;
        }

        public void ChangeType(VehicleType newType)
        {
            Type = newType;
        }

        public bool CanCarry(WeightKg weight, VolumeM3 volume)
        {
            return weight.Value <= WeightAllowance.Value
                && volume.Value <= VolumeAllowance.Value;
        }

        public double EstimateTravelTimeSeconds(double distanceMeters)
        {
            if (distanceMeters <= 0)
                throw new ArgumentException("Distance must be positive", nameof(distanceMeters));

            return distanceMeters / MaxSpeed.Value;
        }

        public void Scrap()
        {
            if (WhenScrapped != null)
            {
                throw new InvalidOperationException($"Vehicle already scrapped.");
            }
            WhenScrapped = DateTime.UtcNow;
        }
    }

}
