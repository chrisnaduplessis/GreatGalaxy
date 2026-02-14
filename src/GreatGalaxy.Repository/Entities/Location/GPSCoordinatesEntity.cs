using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Repository.Entities.Location
{
    /// <summary>
    /// GPS coordinates of location on a celestial body, such as Earth.
    /// It is assumed that some sort of similar systme exists on other celestial bodies, but the details of how it works are not yet defined.
    /// </summary>
    public class GPSCoordinatesEntity
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
