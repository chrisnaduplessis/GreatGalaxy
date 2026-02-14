using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Dispatch.Models
{
    public class CheckpointReached
    {
        public LocationId LocationId { get; }

        public DateTime Timestamp { get; }
    }
}
