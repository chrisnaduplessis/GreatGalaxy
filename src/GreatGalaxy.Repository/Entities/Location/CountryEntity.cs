namespace GreatGalaxy.Repository.Entities.Location
{
    // Defines a country and all of it's locations, in practice this would never be in one collection but for the sake of simplicity we will keep it like this
    public class CountryEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<LocationEntity> Locations { get; set; }
    }
}
