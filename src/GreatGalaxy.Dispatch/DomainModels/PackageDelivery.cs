using GreatGalaxy.Common.ValueTypes.Location;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace GreatGalaxy.Dispatch.DomainModels
{
    public class PackageDelivery
    {
        public Address Destination { get; set; }

        public Address Origin { get; set; }

        public Package Package { get; set; }
    }
}
