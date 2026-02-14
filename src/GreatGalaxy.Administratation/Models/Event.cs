using GreatGalaxy.Dispatch.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Administration.DomainItems
{
    public class Event
    {
        public Guid Id { get; set; }
        
        public EventType Type { get; set; }
    }
}
