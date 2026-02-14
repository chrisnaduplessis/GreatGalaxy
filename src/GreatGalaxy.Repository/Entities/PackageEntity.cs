using GreatGalaxy.Repository.Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Repository.Entities
{
    public class PackageEntity
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public LocationEntity Orgin { get; set; }

        public LocationEntity Destination { get; set; }

        public double WeightKg { get; set; }

        public double VolumeM3 { get; set; }
    }
}
