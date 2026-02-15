using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Common.ValueTypes
{
    public readonly record struct SpeedMetersPerSecond
    {
        public double Value { get; init; }

        public SpeedMetersPerSecond(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Speed cannot be negative.");
            }
            else if (value > 299_792_458)
            {
                throw new ArgumentException("Can't go faster than the speed of light.");
            }
            else
            {
                Value = value;
            }
        }
    }
}
