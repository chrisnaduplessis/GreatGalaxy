using GreatGalaxy.Common.ValueTypes.Event;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GreatGalaxy.Dispatch.DomainModels
{
    public class DeliveryEvent
    {
        public EventCategory EventType { get; set; }

        public DateTime Timestamp { get; set; }

        public TimeSpan Duration { get; set; }

        public string Description { get; set; }

        public List<Package> PackagesDamaged { get; set; } = new List<Package>();

        public List<Package> PackagesLost { get; set; } = new List<Package>();
    }
}
