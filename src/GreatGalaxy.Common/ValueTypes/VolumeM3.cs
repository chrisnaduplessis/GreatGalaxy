namespace GreatGalaxy.Common.ValueTypes
{
    public readonly record struct VolumeM3
    {
        public double Value { get; }

        public VolumeM3(double value)
        {
            if (value <= 0)
                throw new ArgumentException("Volume must be positive", nameof(value));

            Value = value;
        }
    }
}
