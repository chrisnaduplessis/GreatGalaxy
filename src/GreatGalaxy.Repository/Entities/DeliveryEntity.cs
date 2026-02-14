using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Repository.Entities
{
    public class DeliveryEntity
    {
        public string Id { get; set; }

        public RouteEntity Route { get; set; }

        public DriverEntity Driver { get; set; }

        public List<DeliveryCheckpoint> CheckpointReached { get; set; }

        public List<PackageEntity> Packages { get; set; } = new List<PackageEntity>();

        public DateTime Departed { get; set; }

        public DateTime Arrived { get; set; }
    }
}
