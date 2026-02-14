using GreatGalaxy.Repository.Entities;
using LiteDB;

namespace GreatGalaxy.Repository.Repositories
{
    public class BaseRepository<T>
        where T : IEntity
    {
        private readonly LiteDatabase db = new LiteDatabase("GreatGalaxy.db");
        protected readonly ILiteCollection<T> collection;

        public BaseRepository()
        {
            db = new LiteDatabase(@"c:\Temp\GreatGalaxy.db");
            collection = db.GetCollection<T>(nameof(T));
        }
    }
}
