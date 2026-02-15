using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Dispatch.Models
{
    public class CheckpointReached
    {
        public CheckpointReached(LocationId locationId, DateTime time)
        {
            LocationId = locationId;
            Timestamp = time;
        }
        public LocationId LocationId { get; }

        public DateTime Timestamp { get; }
    }
}
