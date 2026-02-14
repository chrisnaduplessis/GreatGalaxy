using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Dispatch.DomainModels
{
    public class Package
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public double Volume { get; set; }
    }
}
