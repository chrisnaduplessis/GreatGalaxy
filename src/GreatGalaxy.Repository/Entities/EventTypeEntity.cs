using GreatGalaxy.Common.ValueTypes.Event;

namespace GreatGalaxy.Repository.Entities
{
    public class EventTypeEntity : IEntity
    {
        public int Id { get; set; }

        public EventCategory Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
