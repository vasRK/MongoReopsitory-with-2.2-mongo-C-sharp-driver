using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Repository
{
    public class MongoRepository : IMongoRepository
    {
        T IMongoRepository.Read<T>(Guid entityId)
        {
            return _db.GetCollection<T>(typeof(T).Name.ToLower()).Find(x => x.Id == entityId).FirstOrDefault();
        }

        T IMongoRepository.Persist<T>(T entity)
        {
            var collection = _db.GetCollection<T>(typeof(T).Name.ToLower());

            var result = collection.ReplaceOne(x => x.Id.Equals(entity.Id), entity, new UpdateOptions { IsUpsert = true });
            return entity;
        }

        void IMongoRepository.Delete<T>(Guid entityId)
        {
            var collection = _db.GetCollection<T>(typeof(T).Name.ToLower()).DeleteOne<T>(x => x.Id.Equals(entityId));
        }

        IQueryable<T> IMongoRepository.Query<T>(DocumentFilter<T> filter)
        {
            return _db.GetCollection<T>(typeof(T).Name.ToLower())
                .Find(filter.Filter)
                .Sort(filter.Sort)
                .Limit(filter.PageSize)
                .Skip((filter.Page - 1) * filter.PageSize)
                .ToList().AsQueryable();
        }

        T IMongoRepository.Update<T>(Guid entityId, UpdateDefinition<T> update)
        {
            _db.GetCollection<T>(typeof(T).Name.ToLower()).UpdateOne<T>(x => x.Id.Equals(entityId), update);
            return ((IMongoRepository)(this)).Read<T>(entityId);
        }

        public MongoRepository()
        {
            var connectionString = "mongodb://localhost:27017";
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase("Users");
        }

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;
    }
}
