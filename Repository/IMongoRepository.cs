using MongoDB.Driver;
using System;
using System.Linq;

namespace Repository
{
    public interface IMongoRepository
    {
        T Read<T>(Guid entityId) where T : IDocument;
        
        T Persist<T>(T entity) where T : IDocument;

        void Delete<T>(Guid entityId) where T : IDocument;

        T Update<T>(Guid entityId,UpdateDefinition<T> update) where T : IDocument;

        IQueryable<T> Query<T>(DocumentFilter<T> filter) where T : IDocument;
    }
}
