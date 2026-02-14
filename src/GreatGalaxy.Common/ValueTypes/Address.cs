using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Common.ValueTypes
{
    public record struct Address(string AddressLine1, string AddressLine2, string Area, string postalCode, string country, string planet);
}
