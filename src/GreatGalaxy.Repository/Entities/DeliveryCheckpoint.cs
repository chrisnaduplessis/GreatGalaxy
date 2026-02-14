using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Repository.Entities
{
    public class DeliveryCheckpoint
    {
        public PackageEntity Package { get; set; }

        public DateTime PickedUp { get; set; }

        public DateTime Delivered { get; set; }
    }
}
