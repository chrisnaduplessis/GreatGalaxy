using GreatGalaxy.Repository.Entities.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Repository.Entities
{
    public class RouteEntity
    {
        public string Id { get; set; }

        public  LocationEntity Origin { get; set; }

        public List<LocationEntity> Checkpoints { get; set; }

        public LocationEntity Destination { get; set; }

        public double Distance { get; set; }
    }
}
