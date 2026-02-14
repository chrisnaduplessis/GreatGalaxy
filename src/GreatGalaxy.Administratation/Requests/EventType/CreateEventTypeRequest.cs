using GreatGalaxy.Common.ValueTypes.Event;

namespace GreatGalaxy.Administration.Requests.EventType
{
    public record CreateEventTypeRequest(string Name, string Description, Common.ValueTypes.Event.EventType Category);
}
