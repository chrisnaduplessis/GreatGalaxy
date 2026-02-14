using GreatGalaxy.Common.ValueTypes.Location;

namespace GreatGalaxy.Administration.Models
{
    public class Country
    {
        public Country(CountryId? id, string name, List<Location> locations)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Country name is required", nameof(name));
            Id = id;
            Name = name;
            Locations = locations ?? new List<Location>();
        }

        public CountryId? Id { get; }

        public string Name { get; }

        public List<Location> Locations { get; }

        public void AddLocation(Location location)
        {
            if (location == null)
                throw new ArgumentNullException(nameof(location));

            Locations.Add(location);
        }
    }
}
