using GreatGalaxy.Repository.Entities;

namespace GreatGalaxy.Repository.Repositories
{
    public interface IBaseRepository<T>
        where T : IEntity
    {
    }
}
