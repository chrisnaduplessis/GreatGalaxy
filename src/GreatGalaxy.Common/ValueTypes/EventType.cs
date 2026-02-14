using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Dispatch.DomainModels
{
    public record struct EventType(string Name, string Description);
}
