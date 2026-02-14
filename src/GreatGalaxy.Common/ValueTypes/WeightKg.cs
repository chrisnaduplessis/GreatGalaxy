namespace GreatGalaxy.Common.ValueTypes
{
    public readonly record struct WeightKg
    {
        public double Value { get; }

        public WeightKg(double value)
        {
            if (value <= 0)
                throw new ArgumentException("Weight must be positive", nameof(value));

            Value = value;
        }
    }
}
