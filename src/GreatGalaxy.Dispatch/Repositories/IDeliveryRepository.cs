using GreatGalaxy.Common.ValueTypes.Delivery;
using GreatGalaxy.Dispatch.Models;
using GreatGalaxy.Repository.Entities;
using GreatGalaxy.Repository.Repositories;

namespace GreatGalaxy.Dispatch.Repositories
{
    public interface IDeliveryRepository: IBaseRepository<DeliveryEntity>
    {
        /// <summary>
        /// Create new delivery
        /// </summary>
        /// <param name="delivery">Delivery</param>
        /// <returns></returns>
        Delivery Create(Delivery delivery);

        bool Update(Delivery delivery);

        Delivery Get(DeliveryId deliveryId);

        IEnumerable<Delivery> GetAll();
    }
}
