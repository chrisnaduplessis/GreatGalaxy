using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Dispatch.DomainModels
{
    public class Delivery
    {
        public int Id { get; set; }

        public Route Route { get; set; }

        public Driver Driver { get; set; }

        public List<Package> Packages { get; set; } = new List<Package>();

    }
}
