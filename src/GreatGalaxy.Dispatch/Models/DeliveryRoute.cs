using GreatGalaxy.Common.ValueTypes.Route;

namespace GreatGalaxy.Dispatch.Models
{
    public class DeliveryRoute
    {
        public DeliveryRoute(RouteId? id, Location origen, List<Location> checkpoints, Location destination, double distance)
        {
            Id = id;
            Origen = origen;
            Checkpoints = checkpoints;
            Destination = destination;
            Distance = distance;
        }
        public RouteId? Id { get; }

        public string Name => $"{Origen.Address.CelestialBody.Name} - {Destination.Address.CelestialBody.Name}";

        public Location Origen { get; }

        public List<Location> Checkpoints { get; }

        public Location Destination { get; }

        public double Distance { get; set; }

        public void AddCheckpoint(Location checkpoint)
        {
            Checkpoints.Add(checkpoint);
        }
    }
}
