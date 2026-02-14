using GreatGalaxy.Common.ValueTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Dispatch.DomainModels
{
    public class Route
    {
        public string Id { get; set; }

        public Address Origen { get; set; }

        public List<Address> Checkpoints { get; set; } = new List<Address>();

        public Address Destination { get; set; }

        public int Distance { get; set; }
    }
}
