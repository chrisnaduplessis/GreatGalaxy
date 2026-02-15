using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Reporting.Models;

namespace GreatGalaxy.Reporting.DomainModel
{
    // This repo will only ever read deliveries to create a trip.
    public class TripReport
    {
        public int Id { get; set; }

        public DeliveryRoute Route{ get; set; }

        public DateTime? Departed { get; set; }

        public DateTime? Arrived { get; set; }

        public Driver Driver { get; set;  }

        public Vehicle Vehicle { get; set;  }

        public List<DeliveryEvent> Events { get; set; }

        public DeliveryStatus Status { get; set; }
    }
}
