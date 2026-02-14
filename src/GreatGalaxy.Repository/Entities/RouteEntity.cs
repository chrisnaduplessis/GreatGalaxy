using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Repository.Entities
{
    public class RouteEntity
    {
        public string Id { get; set; }

        public  AddressEntity Origin { get; set; }

        public List<AddressEntity> Checkpoints { get; set; }

        public AddressEntity Destination { get; set; }

        public double Distance { get; set; }
    }
}
