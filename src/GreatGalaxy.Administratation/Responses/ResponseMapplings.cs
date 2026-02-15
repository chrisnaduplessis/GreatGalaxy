using GreatGalaxy.Administration.Models;

namespace GreatGalaxy.Administration.Responses
{
    public static class DriverMappings
    {
        public static DriverResponse ToDto(this Driver driver)
        {
            return new DriverResponse(
                driver.Id.Value.Value,
                driver.Name,
                driver.Retired,
                driver.ApprovedVehicles?.Select(v => v.Value)
            );
        }

        public static VehicleResponse ToDto(this Vehicle vehicle)
        {
            return new VehicleResponse(
                vehicle.Id.Value.Value,
                new VehicleTypeResponse(vehicle.Type.Make, vehicle.Type.Model),
                vehicle.Description,
                vehicle.WeightAllowance.Value,
                vehicle.VolumeAllowance.Value,
                vehicle.MaxSpeed.Value,
                vehicle.WhenScrapped
            );
        }
    }
}
