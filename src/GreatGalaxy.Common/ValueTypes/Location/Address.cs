using System;
using System.Collections.Generic;
using System.Text;

namespace GreatGalaxy.Common.ValueTypes.Location
{
    public record struct Address(string AddressLine1, string AddressLine2, string City, string PostalCode);
}
