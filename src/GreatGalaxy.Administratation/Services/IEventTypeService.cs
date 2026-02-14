using GreatGalaxy.Administration.DomainItems;
using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Common.ValueTypes.Event;

namespace GreatGalaxy.Administration.Services
{
    public interface IEventTypeService
    {
        public EventType Create(string name, string description, EventCategory category);

        public bool UpdateDescription(EventTypeId eventTypeId, string description);

        public EventType Get(EventTypeId eventTypeId);

        public IEnumerable<EventType> GetAll();

        public IEnumerable<EventType> GetByCategory(EventCategory category);
    }
}
