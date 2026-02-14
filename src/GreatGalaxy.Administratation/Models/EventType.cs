using GreatGalaxy.Common.ValueTypes;
using GreatGalaxy.Common.ValueTypes.Event;

namespace GreatGalaxy.Administration.DomainItems
{
    public class EventType
    {
        public EventTypeId? Id { get; }
        
        public EventCategory Category { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public EventType(EventTypeId? id, EventCategory type, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Event type name is required", nameof(name));
            Id = id;
            Category = type;
            Name = name;
            Description = description;
        }

        public void Rename(string newName)
        {
                if (string.IsNullOrWhiteSpace(newName))
                    throw new ArgumentException("Event type name is required", nameof(newName));
    
                Name = newName;
        }

        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }
    }
}
