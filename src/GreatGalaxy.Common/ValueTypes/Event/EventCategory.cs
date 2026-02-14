using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Common.ValueTypes.Event
{
    public enum EventCategory
    {
        Unknown = 0,
        VehicleDeparture = 1,
        CheckpointReached = 2,
        DestinationReached = 3,
        Information = 4,
        Disruption = 5,
        Disaster = 6,
        CompleteAndUtterFailure = 7
    }
}
