using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Common.ValueTypes.Vehicle
{
    public readonly record struct VehicleType
    {
        public readonly string Make { get; }

        public readonly string Model { get; }

        public VehicleType(string make, string model)
        {
            if (string.IsNullOrWhiteSpace(make))
                throw new ArgumentException("Make cannot be null or whitespace.", nameof(make));
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be null or whitespace.", nameof(model));
            Make = make;
            Model = model;
        }
    }
}
