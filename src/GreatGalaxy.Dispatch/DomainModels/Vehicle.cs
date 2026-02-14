using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Dispatch.DomainModels
{
    public class Vehicle
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public double WeightAllowanceKg { get; init; }

        public double VolumeAllowance { get; init; }

        public double SpeedMetersPerSeccond { get; init; }
    }
}
