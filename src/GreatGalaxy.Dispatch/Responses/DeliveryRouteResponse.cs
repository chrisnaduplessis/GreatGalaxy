using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Dispatch.Models;

namespace GreatGalaxy.Dispatch.Responses
{
    public record DeliveryRouteResponse(int? id, LocationResponse origen, List<LocationResponse> checkpoints, LocationResponse destination, double distance);
}
