using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Administration.Models
{
    public class Location
    {
        public Location(LocationId? id, Address address)
        {
            Id = id;
            Address = address;
        }

        public LocationId? Id { get;}

        public Address Address { get;}
    }
}
