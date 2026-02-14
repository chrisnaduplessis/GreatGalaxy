using GreatGalaxy.Administration.Models;
using GreatGalaxy.Administration.Repositories;
using GreatGalaxy.Common.ValueTypes.Event;

namespace GreatGalaxy.Administration.Services
{
    public class EventTypeService : IEventTypeService
    {
        private readonly IEventTypeRepository eventTypeRepository;

        public EventTypeService(IEventTypeRepository eventTypeRepository)
        {
            this.eventTypeRepository = eventTypeRepository;
        }

        public EventType Create(string name, string description, EventCategory category)
        {
            var eventType = new EventType(null, category, name, description);
            return this.eventTypeRepository.Create(eventType);
        }

        public bool UpdateDescription(EventTypeId eventTypeId, string description)
        {
            var eventType = this.eventTypeRepository.Get(eventTypeId);
            eventType.ChangeDescription(description);
            return this.eventTypeRepository.Update(eventType);
        }

        public EventType Get(EventTypeId eventTypeId)
        {
            return this.eventTypeRepository.Get(eventTypeId);
        }

        public IEnumerable<EventType> GetAll()
        {
            return this.eventTypeRepository.GetAll();
        }

        public IEnumerable<EventType> GetByCategory(EventCategory category)
        {
            return this.eventTypeRepository.GetByCategory(category);
        }
    }
}
