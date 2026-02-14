using GreatGalaxy.Common.ValueTypes.Vehicle;

namespace GreatGalaxy.Administration.Models
{
    public class Planet
    {
        public Planet(PlanetId? id, string name, List<Country>  countries)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Planet name is required", nameof(name));
            Id = id;
            Name = name;
            Countries = countries ?? new List<Country>();
        }

        public PlanetId? Id { get; set; }

        public string Name { get; }

        public List<Country> Countries { get; }

        public void AddCountry(Country country)
        {
            if (country == null)
                throw new ArgumentNullException(nameof(country));
            Countries.Add(country);
        }
    }
}
