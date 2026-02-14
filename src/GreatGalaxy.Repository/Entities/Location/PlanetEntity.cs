namespace GreatGalaxy.Repository.Entities.Location
{
    /// <summary>
    /// Defines a planet and all of it's locations, in practice this would never be in one collection but for the sake of simplicity we will keep it like this
    /// </summary>
    public class PlanetEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CountryEntity> Countries { get; set; }
    }
}
