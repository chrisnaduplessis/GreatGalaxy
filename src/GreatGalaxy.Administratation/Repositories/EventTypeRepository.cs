using GreatGalaxy.Administration.Models;
using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Administration.Repositories
{
    public class EventTypeRepository : BaseRepository<EventTypeEntity>, IEventTypeRepository
    {
        public EventType Create(EventType type)
        {
            var entity = MapToEventTypeEntity(type);
            entity.Id = this.collection.Insert(entity);

            return MapToEventType(entity);
        }

        public EventType Get(EventTypeId eventTypeId)
        {
            return MapToEventType(this.collection.FindById(eventTypeId.Value));
        }

        public IEnumerable<EventType> GetAll()
        {
            return this.collection.FindAll().Select(MapToEventType);
        }

        public IEnumerable<EventType> GetByCategory(EventCategory category)
        {
            return this.collection.Find(_ => _.Category == category).Select(MapToEventType);
        }

        public bool Update(EventType type)
        {
            var entity = MapToEventTypeEntity(type);
            return this.collection.Update(entity);
        }

        private static EventType MapToEventType(EventTypeEntity entity)
        {
            return new EventType(
                new EventTypeId(entity.Id),
                entity.Category,
                entity.Name,
                entity.Description);
        }

        private static EventTypeEntity MapToEventTypeEntity(EventType type)
        {
            return new EventTypeEntity
            {
                Id = type.Id.HasValue ? type.Id.Value.Value : 0,
                Category = type.Category,
                Name = type.Name,
                Description = type.Description,
            };
        }
    }
}
