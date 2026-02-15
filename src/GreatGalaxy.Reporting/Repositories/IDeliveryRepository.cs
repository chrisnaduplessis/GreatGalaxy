using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Reporting.Repositories
{
    public interface IDeliveryRepository: IBaseRepository<DeliveryEntity>
    {
        DeliveryEntity Get(DeliveryId deliveryId);
    }
}
