using GreatGalaxy.Repository.Entities;
using LiteDB;

namespace GreatGalaxy.Repository.Repositories
{
    public class BaseRepository<T>
        where T : IEntity
    {
        private readonly LiteDatabase db;
        protected readonly ILiteCollection<T> collection;

        public BaseRepository()
        {
            db = new LiteDatabase(@"Filename=c:\Temp\GreatGalaxy.db;Connection=shared");
            collection = db.GetCollection<T>(nameof(T));
        }
    }
}
