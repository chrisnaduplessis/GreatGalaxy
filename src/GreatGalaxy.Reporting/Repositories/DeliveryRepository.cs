using GreatGalaxy.Common.Utilities;
using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Common.ValueTypes.Driver;
using GreatGalaxy.Common.ValueTypes.Location;
using GreatGalaxy.Common.ValueTypes.Route;
using GreatGalaxy.Common.ValueTypes.Vehicle;
using GreatGalaxy.Reporting.DomainModel;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Reporting.Repositories
{
    public class DeliveryRepository : BaseRepository<DeliveryEntity>, IDeliveryRepository
    {
        public DeliveryEntity Get(DeliveryId deliveryId)
        {
            return this.collection.FindById(deliveryId.Value);
        }
    }
}
