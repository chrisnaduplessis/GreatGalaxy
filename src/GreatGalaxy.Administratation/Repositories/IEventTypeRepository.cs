using GreatGalaxy.Administration.DomainItems;
using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Common.ValueTypes.Event;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Administration.Repositories
{
    public interface IEventTypeRepository : IBaseRepository<EventTypeEntity>
    {
        /// <summary>
        /// Create new event type
        /// </summary>
        /// <param name="type">Event type</param>
        /// <returns></returns>
        EventType Create(EventType type);

        /// <summary>
        /// Update existing event type
        /// </summary>
        /// <param name="type">Event type</param>
        /// <returns></returns>
        bool Update(EventType type);

        /// <summary>
        /// Get event type by id
        /// </summary>
        /// <param name="eventTypeId">Event type id</param>
        /// <returns></returns>
        EventType Get(EventTypeId eventTypeId);

        IEnumerable<EventType> GetAll();

        IEnumerable<EventType> GetByCategory(EventCategory category);
    }
}
